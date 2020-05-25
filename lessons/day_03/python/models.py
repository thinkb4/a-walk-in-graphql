from sqlalchemy import ForeignKey, Column, Integer, String
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship

Base = declarative_base()


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
    friends = relationship("Person_friends", uselist=True)
    skills = relationship("Person_skills", uselist=True)
    favSkill = Column(String)

class Person_friends(Base):
    __tablename__ = 'person_friends'

    person_id = Column(String, ForeignKey('persons.id'), primary_key=True)
    friend_id = Column(String, primary_key=True)

class Person_skills(Base):
    __tablename__ = 'person_skills'

    person_id = Column(String, ForeignKey('persons.id'), primary_key=True)
    skill_id = Column(String, primary_key=True)
