using System;
using System.IO;
using System.Net;

namespace SurveyMonkeyTests
{
    public class MockHttpWebResponse : InnerMockHttpWebResponse
    {
#pragma warning disable 612
        public MockHttpWebResponse(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }
#pragma warning restore 612
    }

    public class InnerMockHttpWebResponse : HttpWebResponse
    {
        private MemoryStream _stream;
        private HttpStatusCode _statusCode;

        /*
         * Why is this marked as Obsolete?
         * 
         * We have to call one of HttpWebResponse's 3 constructors since we inherit from it. Its main constructor (as
         * used by the framework) is marked internal so can't be accessed. Its second (for serialization) is marked
         * obsolete without an error, but in turn calls into WebResponse which throws a PlatformNotSupportedException.
         * The final parameterless constructor is marked obsolete with an error so in theory can't be called either, but
         * by marking this constructor (which calls it) as Obsolete, we escape that being a compile error because Obsolete
         * methods are allowed to call other Obsolete methods (even though this method's Obsolete is only a warning not
         * an error). Finally, we wrap this InnerMockHttpWebResponse class in MockHttpWebResponse, where we can disable the
         * warning, so that the consumer elsewhere doesn't have to worry about this hackery.
         */
        [Obsolete]
        public InnerMockHttpWebResponse(string message, HttpStatusCode statusCode)
        {
            _stream = new MemoryStream();
            var writer = new StreamWriter(_stream);
            writer.Write(message);
            writer.Flush();
            _stream.Position = 0;
            _statusCode = statusCode;
        }

        public override HttpStatusCode StatusCode { get => _statusCode; }

        public override Stream GetResponseStream()
        {
            return _stream;
        }
    }
}