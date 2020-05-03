import graphene
from datetime import datetime
from flata import Query as FQuery
from data import db


class Skill(graphene.ObjectType):
    """
    This is the Skill description shown in the palyground
    """
    id = graphene.ID() # this is a comment
    parent = graphene.Field(lambda: Skill, description="This defines a relationship with a Skill Object Type value")
    name = graphene.NonNull(graphene.String, description="Just a simple description")
    # name = graphene.String(required=True) # Is the same as the NonNull syntax
    now = graphene.DateTime(deprecation_reason="This is just an example of a virtual field.") # this is a deprecatde field

    # Field-level resolver
    @staticmethod
    def resolve_now(info):
        """
        Resolves now as a virtual field
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolverparaminfo
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
        return tb.get(id=parent['parent']) if parent['parent'] else None


class Person(graphene.ObjectType):
    id = graphene.ID() # this is a comment
    age = graphene.Int()
    eyeColor = graphene.String(name='eyeColor')
    name = graphene.String()
    surname = graphene.String()
    full_name = graphene.String(description="Name and surname concatenation")
    email = graphene.String()
    friends = graphene.List(lambda: Person, description="This is a list of Persons")
    skills = graphene.List(lambda: Skill, description="This is a list of Skills")
    fav_skill = graphene.Field(Skill)

    # Field-level resolver
    @staticmethod
    def resolve_full_name(parent, info):
        return f"{parent['name']} {parent['surname']}"

    @staticmethod
    def resolve_friends(parent, info):
        db.table('persons') # Method table will create or retrieve if it exists
        tb = db.get('persons') #Methos to get the content of the table
        # loop through list of friends from parent using for loop
        friends = []
        for friend in parent['friends']:
            friends.append(tb.get(id=friend))
        return friends
    
    @staticmethod
    def resolve_skills(parent, info):
        db.table('skills') # Method table will create or retrieve if it exists
        tb = db.get('skills') #Methos to get the content of the table
        # loop through list of skills from parent using list comprehension
        skills = [tb.get(id=skill) for skill in parent['skills']]
        return skills
        
    @staticmethod
    def resolve_fav_skill(parent, info):
        db.table('skills') # Method table will create or retrieve if it exists
        tb = db.get('skills') #Methos to get the content of the table
        return tb.get(id=parent['favSkill']) if isinstance(parent['favSkill'], int) else None
