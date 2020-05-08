from graphene import ObjectType, NonNull, String, Field, ID, DateTime
from datetime import datetime
from flata import Query as FQuery
from data import db


class Skill(ObjectType):
    """
    This is the Skill description shown in the palyground
    """
    id = ID()  # this is a comment
    parent = Field(lambda: Skill, description="This defines a relationship with a Skill Object Type value")
    name = NonNull(String, description="Just a simple description")
    # name = String(required=True)  # Is the same as the NonNull syntax
    now = DateTime(deprecation_reason="This is just an example of a virtual field.")

    # Field-level resolver
    @staticmethod
    def resolve_now(info):
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
        db.table('skills')  # Method table will create or retrieve if it exists
        tb = db.get('skills')  # Methos to get the content of the table
        return tb.get(id=parent['parent']) if parent['parent'] else None
