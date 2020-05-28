from ariadne import QueryType, ObjectType
from random import randint
from models import Skill, Person
from data import session
from datetime import datetime

query = QueryType()

# Top level resolvers
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

# Type definition
skill = ObjectType("Skill")
person = ObjectType("Person")

# Field level resolvers
@skill.field("now")
def resolve_now(_, info):
    return datetime.now()

@skill.field("parent")
def resolve_parent(obj, info):
    return obj.parent

@person.field("fullName")
def resolve_full_name(obj, info):
    return f'{obj.name} {obj.surname}'

@person.field("friends")
def resolve_friends(obj, info):
    return obj.friends

@person.field("skills")
def resolve_skills(obj, info):
    return obj.skills

@person.field("favSkill")
def resolve_fav_skill(obj, info):
    return obj.favSkill
