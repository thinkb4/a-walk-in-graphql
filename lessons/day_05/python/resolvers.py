from ariadne import QueryType, ObjectType, EnumType, MutationType
from random import randint
from models import Skill, Person, person_friends, person_skills
from data import session
from datetime import datetime
import uuid


query = QueryType()
mutation = MutationType()

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


def filter_with_input(obj, input):
    if len(input) == 0:
        return obj
    else:
        attr = next(iter(input))
        value = input.pop(next(iter(input)))
        return filter_with_input(
            [x for x in obj if getattr(x, attr) == value], input
        )


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


# Mutations
@mutation.field("createSkill")
def resolve_create_skill(_, info, input):
    skill = Skill()
    skill.id = str(uuid.uuid4())
    [setattr(skill, key, input.get(key)) for key in input.keys()]
    try:
        session.add(skill)
        session.commit()
    except Exception:
        session.rollback()
    return skill


@mutation.field("createPerson")
def resolve_create_person(_, info, input):
    person = Person()
    person.id = str(uuid.uuid4())
    for key in input.keys():
        if key == "friends":
            for friend_key in input.get(key):
                add_friend = person_friends.insert().values(
                    person_id=person.id, friend_id=friend_key
                )
                session.execute(add_friend)
        elif key == "skills":
            for skill_key in input.get(key):
                add_skill = person_skills.insert().values(
                    person_id=person.id, skill_id=skill_key
                )
                session.execute(add_skill)
        else:
            setattr(person, key, input.get(key))
    try:
        session.add(person)
        session.commit()
    except Exception:
        session.rollback()
    return person


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
def resolve_friends(obj, info, input=None):
    return filter_with_input(obj.friends, input) if input else obj.friends


@person.field("skills")
def resolve_person_skills(obj, info, input=None):
    return filter_with_input(obj.skills, input) if input else obj.skills


@person.field("favSkill")
def resolve_fav_skill(obj, info):
    return obj.person_favSkill
