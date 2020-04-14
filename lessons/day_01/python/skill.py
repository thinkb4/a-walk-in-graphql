import graphene
from datetime import datetime
from flata import Query as FQuery
from data import db

class Skill(graphene.ObjectType):
    id = graphene.ID() # this is a comment
    parent = graphene.Field(lambda: Skill, description="we're not resolving the parent for now")
    name = graphene.NonNull(graphene.String, description="Just a simple description")
    # name = graphene.String(required=True) # Is the same as the NonNull syntax
    now = graphene.DateTime(deprecation_reason="This is just an example of a virtual field.") # this is a deprecatde field

    # Field-level resolver
    @staticmethod
    def resolve_now(self, info):
        return datetime.now()

    # Field-level resolver
    @staticmethod
    def resolve_parent(parent, info):
        db.table('skills') # Method table will create or retrieve if it exists
        tb = db.get('skills') #Methos to get the content of the table
        return tb.search(FQuery().id == parent['parent'])[0] if isinstance(parent['parent'], int) else None