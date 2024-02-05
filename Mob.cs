using System.Text;
using Godot;

public partial class Mob : RigidBody2D
{
	private int currentFrame = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimatedSprite2D animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		string[] mobTypes = animatedSprite.SpriteFrames.GetAnimationNames();
		animatedSprite.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		AnimatedSprite2D animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("Collision").Append(animatedSprite.Animation).Append(animatedSprite.Frame);
		
		CollisionPolygon2D currentCollision = GetNode<CollisionPolygon2D>(stringBuilder.ToString());
		currentCollision.Disabled = true;
		currentCollision.Visible = true;
		
		if (currentFrame == animatedSprite.Frame)
		{
			return;
		}

		stringBuilder.Clear();
		stringBuilder.Append("Collision").Append(animatedSprite.Animation).Append(currentFrame);

		CollisionPolygon2D oldCollision = GetNode<CollisionPolygon2D>(stringBuilder.ToString());
		oldCollision.Disabled = false;
		oldCollision.Visible = false;
		
		currentFrame = animatedSprite.Frame;
	}
	
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}

