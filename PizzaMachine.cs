using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Tamaian_Rares_Lab2
{
    class PizzaMachine : Component //sintax for inheritance. noi nu putem asocia evenimente unei clase daca nu e componenta a aplicatiei
    {

        private PizzaType mIngredients;
        public PizzaMachine()
        {
            InitializeComponent();
        }
        public PizzaType Ingredients
        {
            get
            {
                return mIngredients;
            }
            set
            {
                mIngredients = value;
            }
        }
        private System.Collections.ArrayList mPizzas = new System.Collections.ArrayList();
        public Pizza this[int Index] // index concept.. cand vrem ca o colectie de tip lista inlantuita sa fie accesata ca un array
        {
            get
            {
                return (Pizza)mPizzas[Index];
            }
            set
            {
                mPizzas[Index] = value;
            }
        }
        public delegate void PizzaCompleteDelegate(); //creating a custom event aka function pointer imi tine minte fct ce trebuie apelata cand un eveniment e raised pe formular cand e gata o pizza sa fie declarat si sa se tina evidenta
        public event PizzaCompleteDelegate PizzaComplete;
        DispatcherTimer pizzaBakeTimer;
        private void InitializeComponent()
        {
            this.pizzaBakeTimer = new DispatcherTimer();
            this.pizzaBakeTimer.Tick += new System.EventHandler(this.pizzaBakeTimer_Tick);
        }
        private void pizzaBakeTimer_Tick(object sender, EventArgs e)
        {
            Pizza aPizza = new Pizza(this.Ingredients);
            mPizzas.Add(aPizza);
            PizzaComplete();
        }
        public bool Enabled
        {
            set
            {
                pizzaBakeTimer.IsEnabled = value;
            }
        }
        public int Interval
        {
            set
            {
                pizzaBakeTimer.Interval = new TimeSpan(0, 0, value);
            }
        }
        public void MakePizzas(PizzaType dIngredients)
        {
            Ingredients = dIngredients;
            switch (Ingredients)
            {
                case PizzaType.Canibale: Interval = 3; break;
                case PizzaType.Margherita: Interval = 2; break;
                case PizzaType.Pepperoni: Interval = 5; break;
                case PizzaType.Quattro_Stagioni: Interval = 7; break;
                case PizzaType.Veggie: Interval = 4; break;
            }
            pizzaBakeTimer.Start();
        }

    }// end PizzaMachine class aka cuptorul
    enum PizzaType
    {
        Margherita,
        Pepperoni,
        Veggie,
        Quattro_Stagioni,
        Canibale
    }
    class Pizza  //pizza propriu-zisa
    {
        private PizzaType mIngredients; // câmp, privat avem nev de get and set
        public PizzaType Ingredients // proprietate avem nev de ea ca sa accesam campul de mai sus
        {
            get //metoda get
            {
                return mIngredients;
            }
            set //metoda set
            {
                mIngredients = value;
            }
        }
        private float mPrice = .50F;
        private float Price
        {
            get
            {
                return mPrice;
            }
            set
            {
                mPrice = value;
            }
        }
        private readonly DateTime mTimeOfCreation;
        public DateTime TimeOfCreation
        {
            get
            {
                return mTimeOfCreation;
            }
        }
        public Pizza(PizzaType aIngredients) // constructor
        {
            mTimeOfCreation = DateTime.Now;
            mIngredients = aIngredients;
        }
    }// end Pizza class
}
