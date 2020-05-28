from ariadne import QueryType, ObjectType, EnumType
from random import randint
from models import Skill, Person
from data import session
from datetime import datetime

query = QueryType()

def filter_with_input(obj, input):
    if len(input) == 0:
        return obj
    else:
        attr = next(iter(input))
        value = input.pop(next(iter(input)))
        return filter_with_input(list(filter(lambda x: getattr(x, attr) == value, obj)), input)

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

# Type definitions
skill = ObjectType("Skill")
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

# Field level resolvers
@skill.field("now")
def resolve_now(_, info):
    return datetime.now()

@skill.field("parent")
def resolve_parent(obj, info):
    return session.query(Skill).get(obj.parent)

@person.field("fullName")
def resolve_full_name(obj, info):
    return f'{obj.name} {obj.surname}'

@person.field("friends")
def resolve_friends(obj, info, input=None):
    return filter_with_input(obj.friends, input) if input else obj.friends

@person.field("skills")
def resolve_person_skills(obj, info, input=None):
    return filter_with_input(obj.skills, input) if input else obj.skills

@person.field("favSkill")
def resolve_fav_skill(obj, info):
    return session.query(Skill).get(obj.favSkill) if obj.favSkill else None
