using Credit_Card_Fraud_Detection.MLModels;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Credit_Card_Fraud_Detection.Services
{
    public class FraudPredictionService
    {
        private readonly InferenceSession _session;

        public FraudPredictionService(IWebHostEnvironment env)
        {
            var modelPath = Path.Combine(
                env.ContentRootPath,
                "MLModels",
                "fraud_detection_model.onnx"
            );

            _session = new InferenceSession(modelPath);
        }

        public int PredictLabel(OnnxTransactionInput input)
        {
            var inputs = new List<NamedOnnxValue>
        {
        NamedOnnxValue.CreateFromTensor("category",
            new DenseTensor<string>(new[] { input.category }, new[] { 1, 1 })),

        NamedOnnxValue.CreateFromTensor("gender",
            new DenseTensor<string>(new[] { input.gender }, new[] { 1, 1 })),

        NamedOnnxValue.CreateFromTensor("state",
            new DenseTensor<string>(new[] { input.state }, new[] { 1, 1 })),

        NamedOnnxValue.CreateFromTensor("job",
            new DenseTensor<string>(new[] { input.job }, new[] { 1, 1 })),

        NamedOnnxValue.CreateFromTensor("amt",
            new DenseTensor<double>(new[] { input.amt }, new[] { 1, 1 })),

        NamedOnnxValue.CreateFromTensor("city_pop",
            new DenseTensor<long>(new[] { input.city_pop }, new[] { 1, 1 })),

        NamedOnnxValue.CreateFromTensor("hour",
            new DenseTensor<int>(new[] { input.hour }, new[] { 1, 1 })),

        NamedOnnxValue.CreateFromTensor("day_of_week",
            new DenseTensor<int>(new[] { input.day_of_week }, new[] { 1, 1 })),

        NamedOnnxValue.CreateFromTensor("age",
            new DenseTensor<int>(new[] { input.age }, new[] { 1, 1 }))
    };

            using var results = _session.Run(inputs);

            
            return (int)results
                .First(r => r.Name == "output_label")
                .AsTensor<long>()[0];
        }
    }
}
