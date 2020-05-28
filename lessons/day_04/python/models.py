from sqlalchemy import ForeignKey, Column, Integer, String, Table
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship

Base = declarative_base()

person_friends = Table(
    'person_friends', Base.metadata,
    Column('person_id', Integer, ForeignKey('persons.id'), primary_key=True),
    Column('friend_id', Integer, ForeignKey('persons.id'), primary_key=True)
)

person_skills = Table(
    'person_skills', Base.metadata,
    Column('person_id', Integer, ForeignKey('persons.id'), primary_key=True),
    Column('skill_id', Integer, ForeignKey('skills.id'), primary_key=True)
)


class Skill(Base):
    __tablename__ = 'skills'

    id = Column(String, primary_key=True)
    name = Column(String)
    parent = Column(String, ForeignKey('skills.id'))

class Person(Base):
    __tablename__ = 'persons'

    id = Column(String, primary_key=True)
    age = Column(Integer)
    eyeColor = Column(String)
    name = Column(String)
    surname = Column(String)
    email = Column(String)
    friends = relationship(
        "Person",
        secondary=person_friends,
        primaryjoin="Person.id==person_friends.c.person_id",
        secondaryjoin="Person.id==person_friends.c.friend_id"
    )
    skills = relationship("Skill", secondary=person_skills)
    favSkill = Column(String)
