public interface IHealth
{
    //health += amt;
    public void UpdateHealth(int amt);

    //for when hp <= 0;
    public void Die();

    //set hp = maxhp;
    public void InitHealth();
}
