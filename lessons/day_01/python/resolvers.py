from ariadne import QueryType, ObjectType
from random import choice
from models import Skill
from data import session
from datetime import datetime


query = QueryType()

# Type definition
skill = ObjectType("Skill")


# Top level resolver
@query.field("randomSkill")
def resolve_random_skill(_, info):
    records = [skill.id for skill in session.query(Skill.id)]
    return session.query(Skill).get(choice(records))


# Field level resolvers
@skill.field("now")
def resolve_now(_, info):
    return datetime.now()


@skill.field("parent")
def resolve_parent(obj, info):
    return obj.parent_skill
