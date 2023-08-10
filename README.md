# A walk in GraphQL

## Preface

A lot of information has been written about **`GraphQL`** since it was unleashed to the world by **Facebook** in **2015**. As the curious nerds we are, we tend to embrace the **self-taught** approach first, which implies **research**, reading documents and tutorials, and more documents and tutorials, and **trying/error** in a continuous loop, until we feel confident with the topic.  
The intention of this road map is to share with you our own learning experience (one of many paths you can take to grok the language) and to draw you to the highest quality information (which we won't be ever able to write in a better way) in the attempt to prevent you from coming across misleading (when not completely wrong) documents outside in the wild; and keeping you on the **implementation agnostic** spirit of the language, providing **abstract examples** as well as **practical exercises** in different languages so you can **understand the correlations and differences without losing focus on the main topic: `GraphQL`**.

We'll walk through this **starting from the language and the API definition** on the **backend**, gradually **increasing the complexity** as the *walk* develop; it might contain new examples on the future, or more "**days**" including client-side, testing or architecture specific topic, or we might create another *walk* for them, we don't really know.

Hopefully your walk will be much easier than ours!

### Collaborators

- Execution coordinator and reviewer
  - Ezequiel Alvarez - [@ealvarezk](https://github.com/ealvarezk)
- Python exercise contributor and reviewer
  - Ezequiel Tejerina - [@quequitejerina](https://github.com/quequitejerina)
- Java exercise contributor and reviewer
  - Franco Gribaudo - [@fgriba](https://github.com/fgriba)
  - Santiago Ciappesoni [@sciappesoni](https://github.com/sciappesoni)
- NetCore exercise contributors and reviewers
  - Cristian Buffa - [@cristianbuffa](https://github.com/cristianbuffa)
  - José Ignacio Aguilera - [@jiaguilera](https://github.com/jiaguilera)
- Course writer, JavaScript exercise contributor and reviewer
  - Javier Valderrama - [@Jaxolotl](https://github.com/Jaxolotl)

#### Special thanks to our external reviewers!

- Python
  - Eloy Colella - [@ehx](https://github.com/ehx)
- Java
  - Ciro Grbavac - [@ciro599](https://github.com/ciro599)
  - Lucas Mari - [@lucasmari76](https://github.com/lucasmari76)
- NetCore
  - Alejandro Freiberg - [@alejandrofreiberg](https://github.com/alejandrofreiberg)

## Table of contents

- [Intro](introduction/introduction.md)
  - What's a graph?
  - What's GraphQL and what's the `graph` part all about
  - GraphQL vs RESTful
  - Schema Basics
    - SDL - Schema Definition Language
    - Named Types
    - Input and Output Types
    - Lists and Non-nullable
    - Root operation Types
      - Query
      - Mutation
      - Subscription
  - Resolvers
  - Learning resources
- **Setup**
  - [JavaScript](setup/javascript.md)
  - [Java](setup/java.md)
  - [Python](setup/python.md)
  - [NetCore](setup/netcore.md)
- [Day 1](lessons/day_01/day_01.md)
  - Queries and Resolvers
    - Query
    - Resolver
    - Exercise
    - Learning resources
- [Day 2](lessons/day_02/day_02.md)
  - Arguments and Variables
    - Argument
    - Variable
    - Exercise
    - Learning resources
- [Day 3](lessons/day_03/day_03.md)
  - Input Objects and Enums
    - Input Object
    - Enum
    - Exercise
    - Learning resources
- [Day 4](lessons/day_04/day_04.md)
  - Mutations
    - Mutation
    - Description
    - Learning resources
    - Exercise
- [Day 5](lessons/day_05/day_05.md)
  - Interfaces and Unions
    - Interface
    - Union
    - Exercise
    - Learning resources
- [Day 6](lessons/day_06/day_06.md)
  - Extending SDL definitions
    - Extend
    - Scaling our Schema
      - Principled GraphQL
      - Schema Stitching
      - Federation
    - Exercise
    - Learning resources
- [Day 7](lessons/day_07/day_07.md)
  - Errors
    - Description
    - Exercise
    - Learning resources
