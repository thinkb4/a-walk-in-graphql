from ariadne import QueryType, ObjectType
from random import choice
from models import Skill, Person
from data import session
from datetime import datetime


query = QueryType()

# Type definition
skill = ObjectType("Skill")
person = ObjectType("Person")


# Top level resolvers
@query.field("randomSkill")
def resolve_random_skill(_, info):
    records = [skill.id for skill in session.query(Skill.id)]
    return session.query(Skill).get(choice(records))


@query.field("randomPerson")
def resolve_random_person(_, info):
    records = [person.id for person in session.query(Person.id)]
    return session.query(Person).get(choice(records))


@query.field("persons")
def resolve_persons(_, info, id=None):
    return session.query(Person).filter_by(id=id) if id else session.query(Person).all()


# Field level resolvers
@skill.field("now")
def resolve_now(_, info):
    return datetime.now()


@skill.field("parent")
def resolve_parent(obj, info):
    return obj.parent_skill


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
    return obj.person_favSkill
