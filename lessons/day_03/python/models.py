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
    friends = relationship("PersonFriends", uselist=True)
    skills = relationship("PersonSkills", uselist=True)
    favSkill = Column(String, ForeignKey('skills.id'))

class PersonFriends(Base):
    __tablename__ = 'person_friends'

    person_id = Column(String, ForeignKey('persons.id'), primary_key=True)
    friend_id = Column(String, primary_key=True)

class PersonSkills(Base):
    __tablename__ = 'person_skills'

    person_id = Column(String, ForeignKey('persons.id'), primary_key=True)
    skill_id = Column(String, primary_key=True)
