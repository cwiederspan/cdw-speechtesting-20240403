using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

class Program 
{
    async static Task Main(string[] args)
    {
        DotNetEnv.Env.Load();

        string speechEndpoint = Environment.GetEnvironmentVariable("SPEECH_ENDPOINT") ?? throw new ArgumentNullException("SPEECH_ENDPOINT");
        string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY") ?? throw new ArgumentNullException("SPEECH_KEY");

        Console.WriteLine("Speech Endpoint: " + speechEndpoint);

        var speechConfig = SpeechConfig.FromEndpoint(new Uri(speechEndpoint), speechKey);        
        speechConfig.SpeechRecognitionLanguage = "en-US";

        //using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var audioConfig = AudioConfig.FromWavFileInput("audio.wav");
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        Console.WriteLine("Speak into your microphone.");
        var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
        OutputSpeechRecognitionResult(speechRecognitionResult);
    }

    static void OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
    {
        switch (speechRecognitionResult.Reason)
        {
            case ResultReason.RecognizedSpeech:
                Console.WriteLine($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                break;

            case ResultReason.NoMatch:
                Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                break;

            case ResultReason.Canceled:
                var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                }

                break;
        }
    }
}