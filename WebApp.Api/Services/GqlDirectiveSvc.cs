using EntityGraphQL.Compiler;

namespace WebApp.Api.Services
{
    public class WhereClause
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "required by graphql's directives")]
        [System.ComponentModel.Description("Field equal to value")]
        public string? @equal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "required by graphql's directives")]
        [System.ComponentModel.Description("Field contain value")]
        public string? @contains { get; set; }
    }


    public class GqlDirectiveSvc : EntityGraphQL.Directives.DirectiveProcessor<WhereClause>
    {
        public override bool ProcessesResult { get => true; }

        public override string Name { get => "where"; }

        public override string Description { get => "Filter field results"; }

        public override Type GetArgumentsType()
        {
            return typeof(WhereClause);
        }

        public override IGraphQLBaseNode ProcessQuery(GraphQLQueryNode field, WhereClause arguments)
        {
            return base.ProcessQuery(field, arguments);
            // returning null, hide the field
            //return null;
        }

        // setting the ProcessesResult to true, should?? execute this method
        public override object ProcessResult(object value, WhereClause arguments)
        {
            return base.ProcessResult(value, arguments);
        }
    }
}
