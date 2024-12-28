using spacewardeneme2;

public class StrongEnemy : Enemy
{
    public StrongEnemy(int x, int y,int health, int speed, int damage =20 ) : base(x, y, 20, speed, damage) 
    {
        
    }

    public override Image GetImage()
    {
        return spacewardeneme2.Properties.Resources.Gemi_06; // Resmi kaynaklardan al
    }

    public override void Move(int playerX, int playerY)
    {
        // Öklidyen mesafeyi kullanarak oyuncuya doğru hareket et
        double distance = Math.Sqrt(Math.Pow(playerX - X, 2) + Math.Pow(playerY - Y, 2));

        if (distance > 0) // Mesafe sıfırsa hareket etmez
        {
            double stepX = (playerX - X) / distance * Speed;
            double stepY = (playerY - Y) / distance * Speed;

            X += (int)Math.Round(stepX);
            Y += (int)Math.Round(stepY);
        }
    }
    //mermi üretme
    public override Bullet Shoot()
    {
        return new Bullet(X, Y, 10, Direction.Left, 20); 
    }
}
