namespace BO;

public enum OrderStatus { Accepted = 1 /*ממתין ל*/ , Processing /*מעבד*/ , Completed /*הושלם*/ }

public enum CATEGORY
{
    mystery, fantasy, history, scinence, childen, romans, cookingAndBaking, psychology, Kodesh, all
}

public enum Filters
{
    filterBYCategory,
    filterBYName,
    filterBYBiggerThanPrice,
    filterBYSmallerThanPrice,
    None
}


