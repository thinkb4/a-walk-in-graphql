import graphene
from datetime import datetime
from flata import Query as FQuery
from data import db

class Skill(graphene.ObjectType):
    id = graphene.ID() # this is a comment
    parent = graphene.Field(lambda: Skill, description="This defines a relationship with a Skill Object Type value")
    name = graphene.NonNull(graphene.String, description="Just a simple description")
    # name = graphene.String(required=True) # Is the same as the NonNull syntax
    now = graphene.DateTime(deprecation_reason="This is just an example of a virtual field.") # this is a deprecatde field

    # Field-level resolver
    @staticmethod
    def resolve_now(self, info):
        """
        Resolves now as a virtual field
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolverparaminfo
        :param self: refers to the instance when resolving the value
        :param info: refernce to eta information and access to per request context
        """
        return datetime.now()

    # Field-level resolver
    @staticmethod
    def resolve_parent(parent, info):
        """
        Resolves parent Skill
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        """
        db.table('skills') # Method table will create or retrieve if it exists
        tb = db.get('skills') #Methos to get the content of the table
        return tb.search(FQuery().id == parent['parent'])[0] if isinstance(parent['parent'], int) else None
        