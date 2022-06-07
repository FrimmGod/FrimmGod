using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Duel2Botov_Kursa4
{
    public class play
    {

        public event EventHandler NeedLog;


        private Random rnd = new Random();

        public bot b1 = new bot(); //переменная б1 Gordon Freeman
        public bot b2 = new bot(); //переменная б2 GMan
        public bot win = null;
        public void Start(int b1HP, int b1Atack, int b2HP, int b2Atack)
        {
            b1.Born(b1HP, b1Atack);
            b2.Born(b2HP, b2Atack);
        }

        public void WinBonus(bot b) //win bon к боту,который win
        {
            b.HP += rnd.Next(2) + 1; //будет добавляться рандомное число Здоровья от 1 до 2 (Бонус) 
            b.Attack += rnd.Next(2) + 1; //будет добавляться рандомное чсло Атаки от 1 до 2 (Бонус) 
        }

        public void Run(Boolean auto)
        {
            NeedLog("Битва началась", new EventArgs());

            int tur = 1;

            Boolean schans1 = true;

            Boolean schans2 = true;

            while (!b1.isDead() && !b2.isDead()) //Цикл "пока" они живы
            {
                NeedLog("**************** Тур #" + tur.ToString() + " ****************", new EventArgs());

                NeedLog("Ход " + b1.Name , new EventArgs());

                b1.DoTurn(b2); //Б1 ходит на  б2

                NeedLog("Здоровье игрока " + b2.Name +"="+b2.HP.ToString(), new EventArgs());

               

                if (!b2.isDead()) //если б2 не мёртв
                {
                    
                    if(schans2)
                    if (b2.HP < 10) //если у б2 меньше 10 Здоровья, то идет "Второй шанс"
                    {
                        DialogResult dialogResult = MessageBox.Show("Я, " + b2.Name + ", чувствую себя не очень, я не должен сдаваться, дай мне ещё шанс,величайший!", "Второй шанс", MessageBoxButtons.YesNoCancel);
                        if (dialogResult == DialogResult.Yes)
                        {
                            b2.HP += rnd.Next(10) + 1; //1-10 Если "Да"
                            b2.Attack -= rnd.Next(5) + 1;//1-5
                            schans2 = false;
                            NeedLog("Игрок " + b2.Name + " получил второй шанс", new EventArgs());
                            NeedLog("Здоровье игрока " + b2.Name + "=" + b2.HP.ToString(), new EventArgs());
                        }
                        if (dialogResult == DialogResult.Cancel)
                        {
                            if (MessageBox.Show("Закончить игру?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes) return;

                        }
                    }

                    NeedLog("Ход " + b2.Name, new EventArgs());

                    b2.DoTurn(b1);//ход б1 

                    NeedLog("Здоровье игрока " + b1.Name + "=" + b1.HP.ToString(), new EventArgs());

                    if (!b1.isDead()) //если не мёртв
                    {
                        if(schans1)
                        if (b1.HP < 10) //В случае хп меньше 10 Здоровья,идёт второй шанс
                        {

                            DialogResult dialogResult = MessageBox.Show(b1.Name + " молчит", "Второй шанс", MessageBoxButtons.YesNo); //Вопрос персонажа


                            if (dialogResult == DialogResult.Yes)
                            {
                                b2.HP += rnd.Next(10) + 1; //Если "Да",то добавляется рандомное кол-во Здоровья от 1 до 10
                                b2.Attack -= rnd.Next(5) + 1;//Если "Да",то добавляется рандомное кол-во Атаки от 1 до 5
                                schans1 = false;
                                NeedLog("Игрок " + b1.Name + " получил второй шанс", new EventArgs());
                                NeedLog("Здоровье игрока " + b1.Name + "=" + b1.HP.ToString(), new EventArgs());
                            }


                        }
                    }
                    else break;
                }
                else break;
                tur++;
                if(!auto)
                   if (MessageBox.Show("Следующий тур?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.No) break;
            }
            if (b1.HP == b2.HP)
            {
                NeedLog("Ничья ", new EventArgs());
            }
            else
            {
                win = b1;// в том случае,если б1 выйграл
                if (b1.HP < b2.HP) win = b2; //если же первый бот мёртв,то победа =б2



                NeedLog("Выиграл игрок " + win.Name, new EventArgs());

                b1.reBorn();
                b2.reBorn();
                WinBonus(win); //Бонус для победителя

            }          
           

            NeedLog("Конец игры ("+ tur.ToString() + " туров)", new EventArgs());

        }

        public play()
        {

        }
    }
    

    public class bot
    {
        

                                                             //public string log = "";

        private int startHP = 0; //заводим переменную и даём ей стартовое значение 0
        private int startAttack = 0; //заводим переменную и даём ей стартовое значение 0

        private int prevAttack = 0; //заводим переменную и даём ей стартовое значение 0

        
        public string Name = "Анонимус";

        public int HP = 0;
        public int Attack = 0;
        private Random rnd = new Random();

        public bot(string nm)
        {
            Name = nm;
        }
        public bot()
        {

        }

        public Boolean isDead()
        {
            return HP <= 0;
            
        }
        public void  reBorn()
        {
            HP = startHP;
            Attack = startAttack;
        }

        public void Born(int _hp, int _attack) // "Рождение ботов"
        {
            HP = _hp;
            startHP = HP;
            Attack = _attack;
            startAttack = Attack;
        }
        public void DoAttack(bot b)
        {
            prevAttack = rnd.Next(Attack + 1);
                                                            //log += "Aтака=" + prevAttack.ToString()+"\n";

            


            b.HP -= prevAttack;
        }
        public void DoProtect(int atack) //Защита
        {
            int n = rnd.Next(atack + 1);
            HP += n;
        }

        public void DoTurn(bot b)
        {
            DoAttack(b);
            b.DoProtect(prevAttack);
        }

        public override string ToString()
        {
            string s = " Жив";
            if (isDead()) s = " Мертв";
            return Name+" HP="+HP.ToString() + s;
        }
    }
    
}
