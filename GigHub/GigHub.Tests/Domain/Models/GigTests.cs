using FluentAssertions;
using GigHub.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GigHub.Tests.Domain.Models
{
    [TestClass]
    public class GigTests
    {
        // A different approach to write these tests is using BDD. 
        // 
        // GivenAnArtistHasAGig
        //      WhenHeCancelsTheGig
        //          IsCancelledShouldBeSetToTrue
        //          EachAttendeeShouldHaveANotification
        //
        // 
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCancelledToTrue()
        {
            var gig = new Gig();
            
            gig.Cancel();

            gig.IsCancelled.Should().BeTrue();
        }

        [TestMethod]
        public void Cancel_WhenCalled_EachAttendeeShouldHaveANotification()
        {
            var gig = new Gig();
            gig.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "1" }});

            gig.Cancel();

            //TODO: This could be pushed into the Gig class (eg gig.GetAttendees())
            var attendees = gig.Attendances.Select(a => a.Attendee).ToList();
            attendees[0].UserNotifications.Count.Should().Be(1);
        }
    }
}
