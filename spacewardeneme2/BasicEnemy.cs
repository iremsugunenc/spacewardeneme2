using spacewardeneme2;
using System;

public class BasicEnemy : Enemy
{
    // Bu parametrelerle üst sınıf olan Enemy sınıfının constructor'ını çağırır.
    public BasicEnemy(int x, int y, int health, int speed, int damage) : base(x, y,10,speed,damage) 
    {
    }
     // Düşmanın resmini döndüren metod
    public override Image GetImage()
    {
        return  spacewardeneme2.Properties.Resources.Gemi_03; // Resmi kaynaklardan al 
    }
    // Düşmanın hareketini tanımlar
    public override void Move(int playerX, int playerY)
    {
        // Düşman sadece sola hareket eder
        X -= Speed;
    }
    // Düşmanın ateş etmesini sağlar
    public override Bullet Shoot()
    {
        // Düşman sola doğru hareket eden bir mermi oluşturur ve döndürür
        return new Bullet(X, Y, 5, Direction.Left, 10);
    }

}