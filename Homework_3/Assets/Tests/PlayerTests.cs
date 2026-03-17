using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    private GameObject player;

    [SetUp]
    public void Setup()
    {
        player = new GameObject("Player");
    }

    [Test]
    public void PlayerLosesLife_WhenHit()
    {
        //Arrange
        PlayerHealth playerHealth = player.AddComponent<PlayerHealth>();
        playerHealth.redVignetteImage = new GameObject("Vignette").AddComponent<UnityEngine.UI.Image>();
        playerHealth.hearths = new UnityEngine.UI.Image[3];
        for (int i = 0; i < playerHealth.hearths.Length; i++)
        {
            playerHealth.hearths[i] = new GameObject("Heart" + i).AddComponent<UnityEngine.UI.Image>();
        }
        playerHealth.Start();

        //Act
        playerHealth.TakeDamage(1);

        //Assert
        Assert.AreEqual(2, playerHealth.CurrentHealth());
    }

    [Test]
    public void PlayerJumps_OnSpring_YVelocityPositive()
    {
        //Arrange
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        PlayerMovement movement = player.AddComponent<PlayerMovement>();

        movement.Start();

        // Act
        movement.Jump();

        // Assert
        Assert.Greater(movement.GetRigidBody().linearVelocity.y, 0f);
    }

    [TearDown]
    public void TearDown() 
    {
        Object.DestroyImmediate(player);
    }
}
