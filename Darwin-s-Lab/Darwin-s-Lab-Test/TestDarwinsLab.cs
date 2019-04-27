﻿using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;

namespace Darwin_s_Lab.Simulation
{
    [TestClass]
    public class TestDarwinsLab
    {
        [TestMethod]
        public void TestFoodGeneration()
        {
            Canvas canvas = new Canvas();
            canvas.Width = 1000;
            canvas.Height = 1000;

            Map map = new Map(0.8, canvas);

            for (int i=0 ; i < 100 ; i++)
            {
                Food newFood = new Food(canvas,  map);
                Assert.IsTrue(
                    Map.DistanceBetweenTwoPoints(
                        newFood.Position,
                        new Point(500, 500)
                    ) < map.MiddleAreaRadius
                );
            }
        }

        [TestMethod]
        public void TestManager()
        {
            Canvas canvas = new Canvas();
            canvas.Width = 1000;
            canvas.Height = 1000;

            Manager manager = new Manager(canvas, new MainWindow());

            manager.CreateInitialPopulation();
            Assert.AreEqual(manager.CreatureNumber, manager.GetNumberOfCreatures());

            manager.GenerateFood();
            Assert.AreEqual(manager.FoodNumber, manager.GetNumberOfFoods());

            manager.RemoveRottenFood();
            Assert.AreEqual(0, manager.GetNumberOfFoods());
        }

        [TestMethod]
        public void TestManagerPlayPause()
        {
            Canvas canvas = new Canvas();
            canvas.Width = 1000;
            canvas.Height = 1000;

            Manager manager = new Manager(canvas, new MainWindow());

            manager.StartSimulation();
            Assert.IsFalse(manager.IsPaused);

            manager.Pause();
            Assert.IsTrue(manager.IsPaused);

            manager.Resume();
            Assert.IsFalse(manager.IsPaused);

        }

        [TestMethod]
        public void TestStates()
        {
            Canvas canvas = new Canvas();
            canvas.Width = 1000;
            canvas.Height = 1000;

            Manager manager = new Manager(canvas, new MainWindow());
            Assert.IsTrue(
                manager.State is StateInitial
            );

            manager.State.GoNext(manager);
            Assert.IsTrue(
                manager.State is StateGrowFood
            );

            manager.State.GoNext(manager);
            Assert.IsTrue(
                manager.State is StateHunt
            );

            manager.State.GoNext(manager);
            Assert.IsTrue(
                manager.State is StateBackHome
            );

            manager.State.GoNext(manager);
            Assert.IsTrue(
                manager.State is StateReproduce
            );

            manager.State.GoNext(manager);
            Assert.IsTrue(
                manager.State is StateGrowFood
            );
        }
    }
}
