using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace spacewardeneme2
{
    public static class CollisionDetector
    {
        // Oyuncunun uzay aracı ile düşman çarpışma kontrolü
        public static void CheckCollision(SpaceShipcs player, List<Enemy> enemies, Control form)
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = enemies[i];

                // Düşman boyutları
                int enemyWidth = 50;
                int enemyHeight = 50;

                // Çarpışma kontrolü
                if (player.Bounds.IntersectsWith(new Rectangle(enemy.X, enemy.Y, enemyWidth, enemyHeight)))
                {
                    // Çarpışma tespit edildiğinde oyuncunun hasar alması sağlanır
                    player.TakeDamage(enemy.Damage);  // Düşman gemisinin hasarını alır

                    // Düşmanı yok et
                    enemies.RemoveAt(i);

                    if (enemy.PictureBox != null && form.Controls.Contains(enemy.PictureBox))
                    {
                        form.Controls.Remove(enemy.PictureBox);
                        enemy.PictureBox.Dispose();
                    }
                    break; // Bir düşmanla çarpışma bulunduktan sonra işlem sonlandırılır
                }
            }
        }
 
        // Mermilerin düşmanlara ve oyuncuya çarpmasını kontrol et
        public static void CheckBulletCollision(List<Bullet> bullets, List<Enemy> enemies, SpaceShipcs player, Label healthLabel, Control form)
        {
            if (bullets == null || player == null || enemies == null || form == null)
                return; // Null kontrolü

            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bullets[i];
                if (bullet == null)
                    continue;

                // Düşman mermisi ile oyuncu çarpışması kontrolü
                if (bullet.IsEnemy && bullet.Bounds.IntersectsWith(player.Bounds))
                {
                    player.TakeDamage(bullet.Damage); // Oyuncu hasar alır
                    bullets.RemoveAt(i); // Mermi silinir

                    // Sağlık bilgisi güncellenir
                    healthLabel.Text = "Oyuncu Sağlık: " + player.Health.ToString();
                    break;
                }
                else
                {
                    // Oyuncu mermisi ile düşmanlar arasındaki çarpışma kontrolü
                    for (int j = enemies.Count - 1; j >= 0; j--)
                    {
                        Enemy enemy = enemies[j];
                        if (bullet.Bounds.IntersectsWith(enemy.Bounds)) // Düşman ile çarpışma
                        {
                            enemy.TakeDamage(bullet.Damage);  // Düşmana zarar ver
                            bullets.RemoveAt(i);  // Mermiyi sil
                            i--;

                            if (enemy.Health <= 0) // Düşman sağlığı sıfıra düştüyse
                            {
                                enemies.RemoveAt(j); // Düşmanı listeden çıkar
                                form.Controls.Remove(enemy.PictureBox);
                                enemy.PictureBox.Dispose();
                                // Form üzerindeki PictureBoxı kaldır
                                if (enemy.PictureBox != null && form.Controls.Contains(enemy.PictureBox))
                                {
                                    form.Controls.Remove(enemy.PictureBox);
                                    enemy.PictureBox.Dispose();
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
