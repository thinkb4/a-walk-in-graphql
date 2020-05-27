from ariadne import QueryType, ObjectType
from random import randint
from models import Skill, Person
from data import session
from datetime import datetime

query = QueryType()

@query.field("randomSkill")
def resolve_random_skill(_, info):
    records = session.query(Skill).count()
    random_id = str(randint(1, records))
    return session.query(Skill).get(random_id)

@query.field("randomPerson")
def resolve_random_person(_, info):
    records = session.query(Person).count()
    random_id = str(randint(1, records))
    return session.query(Person).get(random_id)

@query.field("persons")
def resolve_persons(_, info, id=None):
    return session.query(Person).filter(Person.id == id).all() if id else session.query(Person).all()

skill = ObjectType("Skill")

@skill.field("now")
def resolve_now(_, info):
    return datetime.now()

@skill.field("parent")
def resolve_parent(obj, info):
    return session.query(Skill).get(obj.parent)

person = ObjectType("Person")

@person.field("fullName")
def resolve_full_name(obj, info):
    return f'{obj.name} {obj.surname}'

@person.field("friends")
def resolve_friends(obj, info):
    ids = [x.friend_id for x in obj.friends]
    return session.query(Person).filter(Person.id.in_(ids)).all()

@person.field("skills")
def resolve_skills(obj, info):
    ids = [x.skill_id for x in obj.skills]
    return session.query(Skill).filter(Skill.id.in_(ids)).all()

@person.field("favSkill")
def resolve_fav_skill(obj, info):
    return session.query(Skill).get(obj.favSkill) if obj.favSkill else None
