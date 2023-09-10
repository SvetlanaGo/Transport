using System;
using System.Collections.Generic;
using System.Text;
using WebLab2.DAL.Entities;
using WebLab2.Models;
using Xunit;

namespace WebLab2.Tests
{
    public class ListViewModelTests
    {
        [Fact]
        public void ListViewModelCountsPages()
        {
            // Act
            var model = ListViewModel<Transport>
            .GetModel(TestData.GetTransportsList(), 1, 3);
            // Assert
            Assert.Equal(2, model.TotalPages);
        }
        [Theory]
        [MemberData(memberName: nameof(TestData.Params),
        MemberType = typeof(TestData))]
        public void ListViewModelSelectsCorrectQty(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<Transport>
            .GetModel(TestData.GetTransportsList(), page, 3);
            // Assert
            Assert.Equal(qty, model.Count);
        }
        [Theory]
        [MemberData(memberName: nameof(TestData.Params),
        MemberType = typeof(TestData))]
        public void ListViewModelHasCorrectData(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<Transport>
            .GetModel(TestData.GetTransportsList(), page, 3);
            // Assert
            Assert.Equal(id, model[0].TransportId);
        }
    }
}
