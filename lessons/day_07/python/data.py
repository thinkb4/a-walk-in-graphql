from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from models import Base, Skill, Person
import json


engine = create_engine('sqlite://')
Session = sessionmaker(bind=engine)
session = Session()

# Setup of in memory database, from data.json if tables doesn´t exist
if not engine.dialect.has_table(engine.connect(), "skills"):
    Base.metadata.create_all(bind=engine)
    data = json.load(open(Path.cwd()/'datasource/data.json'))

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
        for friend in friends:
            new_person.friends.append(friend)
        for skill in skills:
            new_person.skills.append(skill)
        try:
            session.add(new_person)
            session.commit()
        except Exception:
            session.rollback()
