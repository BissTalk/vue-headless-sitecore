using Sitecore.Data.Fields;
using Sitecore.Mvc.Pipelines;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class GetFieldModelArgs : MvcPipelineArgs
    {
        public GetFieldModelArgs(Field field, int recurse)
        {
            Field = field;
            RecursionLevel = recurse;
        }

        public Field Field { get; }

        public object Result { get; set; }

        public int RecursionLevel { get; }
    }
}