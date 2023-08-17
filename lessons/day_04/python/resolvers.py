from typing import Any, Dict, List
from ariadne import QueryType, ObjectType, EnumType
from random import choice

from graphql import GraphQLResolveInfo
from models import Skill, Person
from data import session
from datetime import datetime


query = QueryType()

# Type definitions
skill = ObjectType("Skill")
person = ObjectType("Person")
eye_color = EnumType(
    "EyeColor",
    {
        "BLUE": "blue",
        "GREEN": "green",
        "BROWN": "brown",
        "BLACK": "black",
    },
)


# Top level resolvers
@query.field("randomSkill")
def resolve_random_skill(_, info: GraphQLResolveInfo) -> Skill | None:
    records = [skill.id for skill in session.query(Skill.id)]
    return session.query(Skill).get(choice(records))


@query.field("randomPerson")
def resolve_random_person(_, info: GraphQLResolveInfo) -> Person | None:
    records = [person.id for person in session.query(Person.id)]
    return session.query(Person).get(choice(records))


@query.field("person")
def resolve_person(
    _, info: GraphQLResolveInfo, input: Dict | None = None
) -> Person | None:
    return session.query(Person).filter_by(**input).first() if input else None


@query.field("persons")
def resolve_persons(_, info: GraphQLResolveInfo, input: Dict = {}) -> List[Person]:
    return session.query(Person).filter_by(**input).all()


@query.field("skill")
def resolve_skill(
    _, info: GraphQLResolveInfo, input: Dict | None = None
) -> Skill | None:
    return session.query(Skill).filter_by(**input).first() if input else None


@query.field("skills")
def resolve_skills(_, info: GraphQLResolveInfo, input: Dict = {}) -> List[Skill]:
    return session.query(Skill).filter_by(**input).all()


# Field level resolvers
@skill.field("now")
def resolve_now(_, info: GraphQLResolveInfo) -> datetime:
    return datetime.now()


@skill.field("parent")
def resolve_parent(obj: Any, info: GraphQLResolveInfo) -> Skill:
    return obj.parent_skill


@person.field("fullName")
def resolve_full_name(obj: Any, info: GraphQLResolveInfo) -> str:
    return f"{obj.name} {obj.surname}"


@person.field("friends")
def resolve_friends(
    obj: Any, info: GraphQLResolveInfo, input: Dict = {}
) -> List[Person]:
    return obj.friends.filter_by(**input).all()


@person.field("skills")
def resolve_person_skills(
    obj: Any, info: GraphQLResolveInfo, input: Dict = {}
) -> List[Skill]:
    return obj.skills.filter_by(**input).all()


@person.field("favSkill")
def resolve_fav_skill(obj: Any, info: GraphQLResolveInfo) -> Skill:
    return obj.person_favSkill
