from sqlalchemy import ForeignKey, Column, String
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship


Base = declarative_base()


class Skill(Base):
    __tablename__ = 'skills'

    id = Column(String, primary_key=True)
    name = Column(String)
    parent = Column(String, ForeignKey('skills.id'))
    parent_skill = relationship("Skill", remote_side=[id], uselist=False)
