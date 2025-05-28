namespace Ecomm.CrossCutting.Utils
{
    public static class MapperUtil
    {
        public static void UpdateNonNullProperties<TSource, TTarget>(TSource source, TTarget target)
        {
            var sourceProperties = typeof(TSource).GetProperties();
            var targetProperties = typeof(TTarget).GetProperties().ToDictionary(p => p.Name);

            foreach (var sourceProp in sourceProperties)
            {
                var value = sourceProp.GetValue(source);
                if (value != null && targetProperties.ContainsKey(sourceProp.Name))
                {
                    var targetProp = targetProperties[sourceProp.Name];

                    if (targetProp.CanWrite && targetProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                    {
                        targetProp.SetValue(target, value);
                    }
                }
            }
        }
    }
}