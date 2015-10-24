using UnityEngine;
using System.Collections;

public class Enemy : MovingObject
{
	public int playerDamage;
	private Animator animator;
	private Transform target;
	private bool skipMove;

	public AudioClip enemyAttack1;
	public AudioClip enemyAttack2;
	
	protected override void Start ()
	{
		GameManager.instance.AddEnemyToList (this);
		animator = GetComponent < Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();

		this.FaceToLeft ();
	}

	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		if (skipMove) {
			skipMove = false;
			return;
		}
		base.AttemptMove<T> (xDir, yDir);

		skipMove = true;
	}

	public void MoveEnemy ()
	{
		int xdir = 0;
		int ydir = 0;
		if (Mathf.Abs (target.position.x - transform.position.x) < float .Epsilon) {
			ydir = target.position.y > transform.position.y ? 1 : -1;
		} else {
			xdir = target.position.x > transform.position.x ? 1 : -1;
		}

		if (target.position.x > transform.position.x) {
			this.FaceToRight ();
		} else {
			this.FaceToLeft ();
		}
		
		AttemptMove<Player> (xdir, ydir);
	}

	protected override void OnCantMove <T> (T component)
	{
		Player hitPlayer = component as Player;
		animator.SetTrigger ("enemyAttack");
		hitPlayer.LoseFood (playerDamage);

		SoundManager.instance.RandomizeSfx (enemyAttack1, enemyAttack2);
	}

	private void FaceToLeft() {
		Vector3 newScale = gameObject.transform.localScale;
		newScale.x *= -1;
		gameObject.transform.localScale = newScale;
	}
	
	private void FaceToRight() {
		Vector3 newScale = gameObject.transform.localScale;
		newScale.x *= 1;
		gameObject.transform.localScale = newScale;
	}
}