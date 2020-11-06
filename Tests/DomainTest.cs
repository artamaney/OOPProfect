using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Bot.Domain.Functions;

namespace Tests
{
    [TestFixture]
    public class DomainTest
    {
        [Test]
        public void LessonReminder()
        {
            Assert.AreEqual("Пара сейчас начнется",
                Bot.Domain.Functions.LessonReminder.Do(DateTime.Now.AddMinutes(11)));
        }
    }
}
