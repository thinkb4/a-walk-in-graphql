import graphene
from random import randint
from flata import Query as FQuery
from skill import Skill, db

class Query(graphene.ObjectType):
    random_skill = graphene.Field(Skill)

    # Top-Level resolver
    def resolve_random_skill(self, info):
        db.table('skills')
        tb = db.get('skills')
        id = randint(1, len(tb.all()))
        response = tb.search(FQuery().id == id)[0]
        return response

schema_query = graphene.Schema(query=Query)