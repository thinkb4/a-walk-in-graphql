from typing import Any, List
from ariadne import QueryType, ObjectType
from random import choice

from graphql import GraphQLResolveInfo
from models import Skill, Person
from data import session
from datetime import datetime


query = QueryType()

# Type definition
skill = ObjectType("Skill")
person = ObjectType("Person")


# Top level resolvers
@query.field("randomSkill")
def resolve_random_skill(_, info: GraphQLResolveInfo) -> Skill | None:
    records = [skill.id for skill in session.query(Skill.id)]
    return session.query(Skill).get(choice(records))


@query.field("randomPerson")
def resolve_random_person(_, info: GraphQLResolveInfo) -> Person | None:
    records = [person.id for person in session.query(Person.id)]
    return session.query(Person).get(choice(records))


@query.field("persons")
def resolve_persons(_, info: GraphQLResolveInfo, id: int | None = None) -> List[Person]:
    return (
        session.query(Person).filter_by(id=id).all()
        if id
        else session.query(Person).all()
    )


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
def resolve_friends(obj: Any, info: GraphQLResolveInfo) -> Person:
    return obj.friends


@person.field("skills")
def resolve_skills(obj: Any, info: GraphQLResolveInfo):
    return obj.skills


@person.field("favSkill")
def resolve_fav_skill(obj: Any, info: GraphQLResolveInfo):
    return obj.person_favSkill
