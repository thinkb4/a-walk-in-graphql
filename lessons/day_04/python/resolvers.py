from ariadne import QueryType, ObjectType, gql, EnumType
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

@query.field("person")
def resolve_person(_, info, input=None):
    q = session.query(Person)
    if input:
        for attr, value in input.items():
            q = q.filter(getattr(Person, attr) == value)
    return q.first() if input else None

@query.field("persons")
def resolve_persons(_, info, input=None):
    q = session.query(Person)
    if input:
        for attr, value in input.items():
            q = q.filter(getattr(Person, attr) == value)
    return q.all()

@query.field("skill")
def resolve_skill(_, info, input=None):
    q = session.query(Skill)
    if input:
        for attr, value in input.items():
            q = q.filter(getattr(Skill, attr) == value)
    return q.first() if input else None

@query.field("skills")
def resolve_skills(_, info, input=None):
    q = session.query(Skill)
    if input:
        for attr, value in input.items():
            q = q.filter(getattr(Skill, attr) == value)
    return q.all()

skill = ObjectType("Skill")

@skill.field("now")
def resolve_now(_, info):
    return datetime.now()

@skill.field("parent")
def resolve_parent(obj, info):
    return session.query(Skill).get(obj.parent)

person = ObjectType("Person")

eye_color = EnumType(
    "EyeColor",
    {
        'BLUE': 'blue',
        'GREEN': 'green',
        'BROWN': 'brown',
        'BLACK': 'black',
    },
)

@person.field("fullName")
def resolve_full_name(obj, info):
    return f'{obj.name} {obj.surname}'

@person.field("friends")
def resolve_friends(obj, info, input=None):
    ids = [x.friend_id for x in obj.friends]
    q = session.query(Person)
    q = q.filter(Person.id.in_(ids))
    if input:
        for attr, value in input.items():
            q = q.filter(getattr(Person, attr) == value)
    return q.all()

@person.field("skills")
def resolve_person_skills(obj, info, input=None):
    ids = [x.skill_id for x in obj.skills]
    q = session.query(Skill)
    q = q.filter(Skill.id.in_(ids))
    if input:
        for attr, value in input.items():
            q = q.filter(getattr(Skill, attr) == value)
    return q.all()

@person.field("favSkill")
def resolve_fav_skill(obj, info):
    return session.query(Skill).get(obj.favSkill) if obj.favSkill else None
