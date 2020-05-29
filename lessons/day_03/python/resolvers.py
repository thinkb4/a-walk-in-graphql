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

@query.field("person")
def resolve_person(_, info, id=None):
    return session.query(Person).get(id)

@query.field("skills")
def resolve_skills(_, info, id=None):
    return session.query(Skill).filter_by(id=id) if id else session.query(Skill).all()

@query.field("skill")
def resolve_skill(_, info, id=None):
    return session.query(Skill).get(id)

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
def resolve_friends(obj, info, id=None):
    return [obj for obj in obj.friends if obj.id == id] if id else obj.friends

@person.field("skills")
def resolve_person_skills(obj, info, id=None):
    return [obj for obj in obj.skills if obj.id == id] if id else obj.skills

@person.field("favSkill")
def resolve_fav_skill(obj, info):
    return obj.favSkill
