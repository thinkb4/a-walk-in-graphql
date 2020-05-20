from graphene import ObjectType, NonNull, String, Field, ID, DateTime, Int, List, Argument, Schema
from random import randint
from flata import Query as FQuery, where
from models import Skill, Person, db, InputPerson, InputSkill, validate_input

q = FQuery()


class Query(ObjectType):
    """
    This is a description notation
    @see [GraphQL Spec (June 2018)](https://spec.graphql.org/June2018/#sec-Descriptions)
    """
    random_skill = Field(Skill, required=True,
                         description="This is the random_skill description shown in the palyground")
    random_person = Field(Person, required=True)
    persons = List(Person, input=Argument(InputPerson, required=False))
    person = Field(Person, input=Argument(InputPerson, required=True))
    skills = List(Skill, input=Argument(InputSkill, required=False))
    skill = Field(Skill, input=Argument(InputSkill, required=True))

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
        return tb.search(validate_input(input)) if input else tb.all()

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
        return tb.get(validate_input(input))

    def resolve_skill(parent, info, input):
        """
        Resolves a random skill
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        """
        db.table('skills')
        tb = db.get('skills')
        return tb.get(validate_input(input))

    def resolve_skills(parent, info, input=None):
        """
        Resolves a list of skills
        https://docs.graphene-python.org/en/latest/types/objecttypes/#resolvers
        :param parent: information of the parent instance
        :param info: refernce to eta information and access to per request context
        :param id: is the id passed as an argument, with default value in case id gets empty
        """
        db.table('skills')
        tb = db.get('skills')
        return tb.search(validate_input(input)) if input else tb.all()

schema_query = Schema(query=Query)
