using Sitecore.Data.Fields;
using Sitecore.Mvc.Pipelines;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class GetFieldModelArgs : MvcPipelineArgs
    {
        public GetFieldModelArgs(Field field)
        {
            Field = field;
        }

        public Field Field { get; }

        public object Result { get; set; }
    }
}