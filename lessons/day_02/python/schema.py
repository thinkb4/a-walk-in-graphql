import graphene
from random import randint
from flata import Query as FQuery
from models import Skill, Person, db


class Query(graphene.ObjectType):
    """
    This is a description notation
    @see [GraphQL Spec (June 2018)](https://spec.graphql.org/June2018/#sec-Descriptions)
    """
    random_skill = graphene.Field(Skill, required=True, description="This is the random_skill description shown in the palyground")
    random_person = graphene.Field(Person, required=True)
    persons = graphene.List(Person, id = graphene.Argument(graphene.ID, required=False))

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
        # return tb.search(FQuery().id == parent['parent'])[0] if isinstance(parent['parent'], int) else None 

    def resolve_random_person(self, info):
        """
        Resolves a random person
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        """
        db.table('persons')
        tb = db.get('persons')
        random_id = str(randint(1, len(tb)))
        return tb.get(id=random_id) #this way is more directly and cannot use two conditions. 
        # these ca be used in flata for search in queries
        # return tb.search(FQuery().id == random_id)[0] 

    def resolve_persons(self, info, id=None):
        """
        Resolves a list of persons
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        :param id: is the id passed as an argument, with default value in case id gets empty
        """
        db.table('persons')
        tb = db.get('persons')
        # in the below we use the seach statement because we targeting for a list 
        # of all persons or a list of one person
        return tb.search(FQuery().id == id) if id else tb.all()

schema_query = graphene.Schema(query=Query)
