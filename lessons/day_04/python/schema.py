from graphene import ObjectType, NonNull, String, Field, ID, DateTime, Int, List, Argument, Schema
from random import randint
from flata import Query as FQuery, where
from models import Skill, Person, db, InputPerson, InputSkill

q = FQuery()

def validate_input(obj, value):
    if getattr(obj, value):
        print(obj)
        return getattr(q, value) == getattr(obj, value)

class Query(ObjectType):
    """
    This is a description notation
    @see [GraphQL Spec (June 2018)](https://spec.graphql.org/June2018/#sec-Descriptions)
    """
    random_skill = Field(Skill, required=True,
                         description="This is the random_skill description shown in the palyground")
    random_person = Field(Person, required=True)
    persons = List(Person, input=Argument(InputPerson, required=True))
    person = Field(Person, input=Argument(InputPerson, required=True))
    skills = List(Skill, id=Argument(ID, required=False))
    skill = Field(Skill, id=Argument(ID, required=True))

    # Top-Level resolver
    def resolve_random_skill(parent, info):
        """
        Resolves a random skill
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        """
        db.table('skills')
        tb = db.get('skills')
        random_id = str(randint(1, len(tb)))
        return tb.get(id=random_id)

    def resolve_random_person(parent, info):
        """
        Resolves a random person
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        """
        db.table('persons')
        tb = db.get('persons')
        random_id = str(randint(1, len(tb)))
        return tb.get(id=random_id)

    def resolve_persons(parent, info, input=None):
        """
        Resolves a list of persons
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        :param id: is the id passed as an argument, with default value in case id gets empty
        """
        db.table('persons')
        tb = db.get('persons')
        all_persons = tb.all()
        return tb.search(q.id == input.id)

    def resolve_person(parent, info, input):
        """
        Resolves a single person determinated by the id parameter
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        :param id: is the id passed as an argument
        """
        db.table('persons')
        tb = db.get('persons')
        all_persons = tb.all()
        # for person in all_persons:
        #     print(input, type(input))
        #     print(person, type(person))
        # return tb.search(q.id == input.id)
        my_query = validate_input(input, 'id')
        my_query1 = validate_input(input, 'age')
        print(my_query & my_query1)
        print(my_query1)
        # my_query += (q.age == input.age) if input.age else ''
        # my_query += '&', (q.eyeColor == input.eyeColor) if input.eyeColor else ''
        # my_query += '&', (q.favSkill == input.favSkill) if input.favSkill else ''
        return tb.search(my_query)

    def resolve_skill(parent, info, id=None):
        """
        Resolves a random skill
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        """
        db.table('skills')
        tb = db.get('skills')
        return tb.get(id=id) if id else tb.all()

    def resolve_skills(parent, info, id=None):
        """
        Resolves a list of skills
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        :param id: is the id passed as an argument, with default value in case id gets empty
        """
        db.table('skills')
        tb = db.get('skills')
        return tb.search(FQuery().id == id) if id else tb.all()

schema_query = Schema(query=Query)
