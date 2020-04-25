using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MissionGeneratorTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void DraftHostiles_Should_NotDraft_AbovePlayerLevel()
        {

            //Arrange
            MissionDraftConfiguration missionDraftConfiguration = new MissionDraftConfiguration()
            {
                MaxHostiles = 1000//TODO:dodać intrfejs do losujacego
            };

            int playerLevel = 1;
            var draftingPoolsMock = new Mock<IDraftingPools>();
            draftingPoolsMock.Setup(dp => dp.Config_Hostile_Pool).Returns(new List<HostileDraftConfiguration>
            {
                new HostileDraftConfiguration("Enemy lvl2",0,1,1,1,1,2,new List<ActionData>())
            });
            List<HostileData> result;

            //Act

            var sut = new MissionGenerator(draftingPoolsMock.Object, new UnityRandomGenerator());
            result = sut.DraftHostiles(missionDraftConfiguration, playerLevel);

            //Assert
            Assert.IsTrue(result.Count == 0);
        }

        [Test]
        public void DraftHostiles_Should_Draft_ForPlayerLevel()
        {

            //Arrange
            MissionDraftConfiguration missionDraftConfiguration = new MissionDraftConfiguration()
            {
                MinHostiles = 20,
                MaxHostiles = 25//TODO:dodać intrfejs do losujacego
            };

            int playerLevel = 2;
            var draftingPoolsMock = new Mock<IDraftingPools>();
            draftingPoolsMock.Setup(dp => dp.Config_Hostile_Pool).Returns(new List<HostileDraftConfiguration>
            {
                new HostileDraftConfiguration("Enemy lvl1",0,1,1,1,1,1,new List<ActionData>()),
                new HostileDraftConfiguration("Enemy lvl2",0,1,1,1,1,2,new List<ActionData>()),
                new HostileDraftConfiguration("Enemy lvl3",0,1,1,1,1,3,new List<ActionData>())
            });
            List<HostileData> result;

            //Act

            var sut = new MissionGenerator(draftingPoolsMock.Object, new UnityRandomGenerator());
            result = sut.DraftHostiles(missionDraftConfiguration, playerLevel);

            //Assert
            Assert.IsFalse(result.Any(h => h.MinPlayerLevel > playerLevel));
        }


        //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        //// `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator NewTestScriptWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
