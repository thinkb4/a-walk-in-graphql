from graphene import ObjectType, ID, Field, NonNull, String, DateTime, Int, List, Argument, Enum, InputObjectType
from datetime import datetime
from flata import Query as FQuery, where
from data import db

q = FQuery()

def validate_input(obj):
    if len(obj.keys()) == 1:
        return getattr(q, list(obj.keys())[0]) == getattr(obj, list(obj.keys())[0])
    else:
        key = list(obj.keys())[0]
        element = obj.pop(list(obj.keys())[0])
        return (getattr(q, key) == element) & validate_input(obj)


class Skill(ObjectType):
    """
    This is the Skill description shown in the palyground
    """
    id = ID()
    parent = Field(lambda: Skill, description="This defines a relationship with a Skill Object Type value")
    name = String(required=True, description="Just a simple description")
    now = DateTime(deprecation_reason="This is just an example of a virtual field.")

    def resolve_now(parent, info):
        """
        Resolves now as a virtual field
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolverparaminfo
        :param info: refernce to eta information and access to per request context
        """
        return datetime.now()

    def resolve_parent(parent, info):
        """
        Resolves parent Skill
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        """
        db.table('skills')
        tb = db.get('skills')
        return tb.get(id=parent['parent']) if parent['parent'] else None


class EyeColor(Enum):
    BLUE = 'blue'
    GREEN = 'green'
    BROWN = 'brown'
    BLACK = 'black'


class InputPerson(InputObjectType):
    id = ID()
    age = Int()
    eyeColor = Field(EyeColor)
    favSkill = ID()


class InputSkill(InputObjectType):
    id = ID()
    name = String()


class Person(ObjectType):
    id = ID()
    age = Int()
    eyeColor = Field(EyeColor)
    name = String()
    surname = String()
    full_name = String(description="Name and surname concatenation")
    email = String()
    friends = List(
        lambda: Person, input=Argument(InputPerson, required=True),
        description="This is a list of Persons")
    skills = List(
        Skill, input=Argument(InputSkill, required=True),
        description="This is a list of Skills")
    fav_skill = Field(Skill)

    def resolve_full_name(parent, info):
        return f"{parent['name']} {parent['surname']}"

    def resolve_friends(parent, info, input=None):
        db.table('persons')
        tb = db.get('persons')
        print(parent['friends'])
        # print(tb.search(q.id.any(['1', '3'])))
        print(tb.search(q.id.any(parent['friends'])))
        return tb.search(validate_input(input) & q.id.any(parent['friends']))

    def resolve_skills(parent, info, input=None):
        db.table('skills')
        tb = db.get('skills')
        print(parent['skills'])
        # print(tb.search(q.id == '47'))
        # print(tb.search(q.id == '107'))
        # print(tb.search(q.id.any(['47', '107'])))
        print(tb.search(q.id.any(parent['skills'])))
        return tb.search(validate_input(input) | q.id.any(parent['skills']))

    def resolve_fav_skill(parent, info):
        db.table('skills')
        tb = db.get('skills')
        return tb.get(id=parent['favSkill']) if parent['favSkill'] else None
