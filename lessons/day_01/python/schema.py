import graphene
from random import randint
# from flata import Query as FQuery
from models import Skill, db


class Query(graphene.ObjectType):
    """
    This is a description notation
    @see [GraphQL Spec (June 2018)](https://spec.graphql.org/June2018/#sec-Descriptions)
    """
    random_skill = graphene.Field(Skill, required=True, description="This is the random_skill description shown in the palyground")

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
        random_id = str(randint(1, len(tb)))
        return tb.get(id=random_id) #this way is more directly and cannot use two conditions. 
        # these ca be used in flata for search in queries
        # return tb.search(FQuery().id == random_id)[0] 


schema_query = graphene.Schema(query=Query)
