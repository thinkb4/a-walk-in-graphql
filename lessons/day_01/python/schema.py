import graphene
from random import randint
from flata import Query as FQuery
from skill import Skill, db


class Query(graphene.ObjectType):
    random_skill = graphene.Field(Skill)

    # Top-Level resolver
    def resolve_random_skill(self, info):
        """
        Resolves a random skill
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        """
        db.table('skills')
        tb = db.get('skills')
        random_id = randint(1, len(tb))
        return tb.get(id=random_id)

schema_query = graphene.Schema(query=Query)
