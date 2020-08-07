from ariadne import QueryType, ObjectType
from random import randint
from models import Skill
from data import session
from datetime import datetime


query = QueryType()

# Type definition
skill = ObjectType("Skill")


# Top level resolver
@query.field("randomSkill")
def resolve_random_skill(_, info):
    records = session.query(Skill).count()
    random_id = str(randint(1, records))
    return session.query(Skill).get(random_id)


# Field level resolvers
@skill.field("now")
def resolve_now(_, info):
    return datetime.now()


@skill.field("parent")
def resolve_parent(obj, info):
    return obj.parent_skill
