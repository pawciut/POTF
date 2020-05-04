using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SoldierDataTests
    {
        [Test]
        public void PowerProgressPercent_Should_Return_Quarter()
        {
            //Arrange
            SoldierData soldierData = new SoldierData();
            //Current Attribute level
            soldierData.Power = 0;
            //current attribute progress value total
            soldierData.PowerProgress = 25;
            
            float expectedProgress = 0.25f;

            //Based on the constant that from level 0->1 there is 0-100 progress
            float attributeProgressPercent = 0;


            //Act
            attributeProgressPercent = soldierData.GetAttributeProgressPercent(AttributeTypes.Power);
            

            //Assert
            Assert.AreEqual(expectedProgress, attributeProgressPercent);
        }

        [Test]
        public void HPPercent_Should_Return_Quarter()
        {
            //Arrange
            SoldierData soldierData = new SoldierData();
            soldierData.CurrentHp = 1;
            soldierData.MaxHp = 4;

            float expectedHPPercent = 0.25f;

            float calculatedHPPercent = 0;


            //Act
            calculatedHPPercent = soldierData.GetHpPercent();


            //Assert
            Assert.AreEqual(expectedHPPercent, calculatedHPPercent);
        }

        [Test]
        public void LevelExp_Should_Return_Quarter()
        {
            //Arrange
            SoldierData soldierData = new SoldierData();
            soldierData.CurrentExp = 25;
            soldierData.CurrentLevel = 1;

            float expectedLevelExpPercent = 0.25f;

            //Based on the constant that from level 0->1 there is 0-100 progress
            float calculatedLevelExpPercent = 0;


            //Act
            calculatedLevelExpPercent = soldierData.GetLevelPercent();


            //Assert
            Assert.AreEqual(expectedLevelExpPercent, calculatedLevelExpPercent);
        }
    }
}
