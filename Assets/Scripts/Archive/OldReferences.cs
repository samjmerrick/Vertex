////rotation
//Vector3 difference = pos + (new Vector3(0, -1)) - transform.position;
//float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
//transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);

//public void SpawnAlienLine()
//{
//    int spawnType = Random.Range(0, 5);

//    // A normal spawn line
//    if (spawnType != 4 || GameController.score < 25)
//    {
//        int toSpawn = Random.Range(1, 5); // between 1 - 4

//        for (int i = 0; i < toSpawn; i++)
//        {
//            int chance = Random.Range(0, 2);

//            if (chance != 0)
//            {
//                float x = (i * 0.6f) + pos.x;
//                float xr = ((i * -1) * 0.6f) + pos.x;

//                Instantiate(invader, new Vector3(x, 5f), Quaternion.identity, transform);
//                if (i != 0)
//                {
//                    Instantiate(invader, new Vector3(xr, 5f), Quaternion.identity, transform);
//                }
//                enemiesRemaining += 2;
//            }
//        }
//    }
//    // Otherwise, we spawn a boss
//    else
//    {
//        GameObject Invader = Instantiate(invader, new Vector3(0 + pos.x, 5f), Quaternion.identity, transform);
//        Invader.transform.localScale = new Vector3(3, 3);
//    }
//}

//void OnTriggerEnter2D(Collider2D c)
//{
//    if (!collided)
//    {
//        if (c.gameObject.tag.Equals("Bullet") || c.gameObject.tag.Equals("Shield"))
//        {
//            collided = true;

//            // Destroy the current asteroid
//            Destroy(gameObject);


//            // If large asteroid spawn new ones
//            if (transform.localScale.x > 0.4f)
//            {
//                // Spawn small asteroids
//                Instantiate(mediumAsteroid, new Vector3(transform.position.x + .75f, transform.position.y + .75f, 0), Quaternion.Euler(0, 0, Random.Range(180, 200)));
//                Instantiate(mediumAsteroid, new Vector3(transform.position.x - .75f, transform.position.y + .0f, 0), Quaternion.Euler(0, 0, 180));
//                Instantiate(mediumAsteroid, new Vector3(transform.position.x - .75f, transform.position.y + .75f, 0), Quaternion.Euler(0, 0, Random.Range(160, 180)));
//            }

//            // If medium asteroid spawn new ones
//            if (name.Equals("medium-asteroid(Clone)"))
//            {
//                // Spawn small asteroids
//                Instantiate(smallAsteroid, new Vector3(transform.position.x + .5f, transform.position.y + .5f, 0), Quaternion.Euler(0, 0, Random.Range(180, 200)));
//                Instantiate(smallAsteroid, new Vector3(transform.position.x - .5f, transform.position.y + .0f, 0), Quaternion.Euler(0, 0, 180));
//                Instantiate(smallAsteroid, new Vector3(transform.position.x - .5f, transform.position.y + .5f, 0), Quaternion.Euler(0, 0, Random.Range(160, 180)));
//            }
//        }
//    }
//}