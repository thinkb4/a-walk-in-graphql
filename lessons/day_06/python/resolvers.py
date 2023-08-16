from typing import Any
from ariadne import (
    QueryType,
    ObjectType,
    EnumType,
    MutationType,
    InterfaceType,
    UnionType
)
from random import choice

from graphql import GraphQLResolveInfo
from models import Skill, Person
from data import session
from datetime import datetime
from uuid import uuid4


# Type definitions
query = QueryType()
mutation = MutationType()
person = InterfaceType("Person")
skill = ObjectType("Skill")
# person = ObjectType("Person")
eye_color = EnumType(
    "EyeColor",
    {
        'BLUE': 'blue',
        'GREEN': 'green',
        'BROWN': 'brown',
        'BLACK': 'black',
    },
)
global_search = UnionType("GlobalSearch")


def create_persons(info: GraphQLResolveInfo, input):
    friends = []
    skills = []
    if 'friends' in input:
        person_ids = input.pop('friends')
        friends = session.query(Person).filter(Person.id.in_(person_ids)).all()
    if 'skills' in input:
        skill_ids = input.pop('skills')
        skills = session.query(Skill).filter(Skill.id.in_(skill_ids)).all()

    new_person = Person(**input)
    new_person.id = str(uuid4())
    new_person.friends = friends
    new_person.skills = skills
    if info.return_type.of_type.name == 'Engineer':
        new_person.employeeId = str(uuid4())
    try:
        session.add(new_person)
        session.commit()
    except Exception:
        session.rollback()
    return new_person


@person.type_resolver
def resolve_person_interface(obj: Any, *_):
    if obj.grade:
        return "Engineer"
    if obj.targetGrade:
        return "Candidate"
    return "Contact"


@global_search.type_resolver
def resolve_global_search_type(obj: Any, *_):
    if isinstance(obj, Skill):
        return "Skill"
    if isinstance(obj, Person):
        if obj.grade:
            return "Engineer"
        if obj.targetGrade:
            return "Candidate"
        return "Contact"
    return None


# Top level resolvers
@query.field("randomSkill")
def resolve_random_skill(_, info: GraphQLResolveInfo):
    records = [skill.id for skill in session.query(Skill.id)]
    return session.query(Skill).get(choice(records))


@query.field("randomPerson")
def resolve_random_person(_, info: GraphQLResolveInfo):
    records = [person.id for person in session.query(Person.id)]
    return session.query(Person).get(choice(records))


@query.field("person")
def resolve_person(_, info: GraphQLResolveInfo, input=None):
    return session.query(Person).filter_by(**input).first() if input else None


@query.field("persons")
def resolve_persons(_, info: GraphQLResolveInfo, input={}):
    return session.query(Person).filter_by(**input).all()


@query.field("skill")
def resolve_skill(_, info: GraphQLResolveInfo, input=None):
    return session.query(Skill).filter_by(**input).first() if input else None


@query.field("skills")
def resolve_skills(_, info: GraphQLResolveInfo, input={}):
    return session.query(Skill).filter_by(**input).all()


@query.field("search")
def resolve_search(_, info: GraphQLResolveInfo, input):
    persons = session.query(Person).filter(Person.name.like(f'%{input["name"]}%')).all()
    skills = session.query(Skill).filter(Skill.name.like(f'%{input["name"]}%')).all()
    return persons + skills


# Mutations
@mutation.field("createSkill")
def resolve_create_skill(_, info: GraphQLResolveInfo, input):
    new_skill = Skill(**input)
    new_skill.id = str(uuid4())
    try:
        session.add(new_skill)
        session.commit()
    except Exception:
        session.rollback()
    return new_skill


@mutation.field("createPerson")
def resolve_create_person(_, info: GraphQLResolveInfo, input):
    return create_persons(info, input)


@mutation.field("createCandidate")
def resolve_create_candidate(_, info: GraphQLResolveInfo, input):
    return create_persons(info, input)


@mutation.field("createEngineer")
def resolve_create_engineer(_, info: GraphQLResolveInfo, input):
    return create_persons(info, input)


# Field level resolvers
@skill.field("now")
def resolve_now(_, info: GraphQLResolveInfo):
    return datetime.now()


@skill.field("parent")
def resolve_parent(obj: Any, info: GraphQLResolveInfo):
    return obj.parent_skill


@person.field("fullName")
def resolve_full_name(obj: Any, info: GraphQLResolveInfo):
    return f'{obj.name} {obj.surname}'


@person.field("friends")
def resolve_friends(obj: Any, info: GraphQLResolveInfo, input={}):
    return obj.friends.filter_by(**input).all()


@person.field("skills")
def resolve_person_skills(obj: Any, info: GraphQLResolveInfo, input={}):
    return obj.skills.filter_by(**input).all()


@person.field("favSkill")
def resolve_fav_skill(obj: Any, info: GraphQLResolveInfo):
    return obj.person_favSkill
