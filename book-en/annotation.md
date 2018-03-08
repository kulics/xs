# Annotation
Annotations are declarative tags that are used to pass information about the characteristics of various elements (such as packages, functions, components, etc.) in a program at run time.
Often, we use annotation features in many scenes of reflection and data parsing.

Annotation need to use namespace
        
        System.ComponentModel.DataAnnotations.Schema;
        System.ComponentModel.DataAnnotations;

## Annotation Declaration
As opposed to the comment block, we simply wrap the tagged content with the `\*` and `*\`.
Note that it is valid before the identifier.

Let's take a look at the database data for how to use annotations.

E.g:

        \*Table("test")*\
        Annotation => #~()
        {
                \*Key, Column("id")*\
                Id => "";
                \*Column("name")*\
                Name => "";
                \*Column("data")*\
                Data => "";
        };

We declare an annotation package that annotates the table name `test`, the primary key `id`, the field `name`, and the field `data`.

When processing database, it can be resolved by the database interface to the corresponding name for data manipulation.

We use this package directly inside the program, the program will be automatically mapped to the corresponding database data when calling the database function.
This greatly saves us the work of analytical conversion.

### [Next Chapter](namespace.md)