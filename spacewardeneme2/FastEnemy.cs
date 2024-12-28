using spacewardeneme2;

public class FastEnemy : Enemy
{
    public FastEnemy(int x, int y, int health, int speed,int damage) : base(x, y,5,speed, damage=20)
    {
    }
    public override Image GetImage()
    {
        return spacewardeneme2.Properties.Resources.Gemi_04; // Resmi kaynaklardan al
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

    public override Bullet Shoot()
    {
        //mermi üretilir
        return new Bullet(X, Y, 5, Direction.Left, 10); 
    }
}
