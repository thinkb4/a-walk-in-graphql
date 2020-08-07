from sqlalchemy import create_engine, event
from sqlalchemy.orm import sessionmaker
from models import Base, Skill, Person
from pathlib import Path
import json
from sqlalchemy.engine import Engine


engine = create_engine('sqlite://')
Session = sessionmaker(bind=engine)
session = Session()

# This is only for proper functioning with FK in SQLite
@event.listens_for(engine, "connect")
def set_sqlite_pragma(dbapi_connection, connection_record):
    cursor = dbapi_connection.cursor()
    cursor.execute("PRAGMA foreign_keys=ON")
    cursor.close()

# Setup of in memory database, from data.json if tables doesn´t exist
if not engine.dialect.has_table(engine.connect(), "skills"):
    Base.metadata.create_all(bind=engine)
    data = json.load(open(Path.cwd().parent / 'datasource/data.json'))

    skills = data['skills']
    for skill in skills:
        new_skill = Skill(**skill)
        session.add(new_skill)
        session.commit()

    persons = data['persons']
    for person in persons:
        friends = []
        skills = []
        if 'friends' in person:
            person_ids = person.pop('friends')
            friends = session.query(Person).filter(Person.id.in_(person_ids)).all()
        if 'skills' in person:
            skill_ids = person.pop('skills')
            skills = session.query(Skill).filter(Skill.id.in_(skill_ids)).all()

        new_person = Person(**person)
        new_person.friends = friends
        new_person.skills = skills
        try:
            session.add(new_person)
            session.commit()
        except Exception:
            session.rollback()
