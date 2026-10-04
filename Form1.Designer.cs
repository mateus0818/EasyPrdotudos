namespace EasyProdutos
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textNome = new TextBox();
            textQuantidade = new TextBox();
            textPreço = new TextBox();
            btnCadastrar = new Button();
            btnBuscar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(319, 212);
            label1.Name = "label1";
            label1.Size = new Size(105, 17);
            label1.TabIndex = 0;
            label1.Text = "Nome do produto";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(319, 268);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 1;
            label2.Text = "Quantidade";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(319, 324);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 2;
            label3.Text = "Preço";
            // 
            // textNome
            // 
            textNome.Location = new Point(454, 209);
            textNome.Name = "textNome";
            textNome.Size = new Size(100, 23);
            textNome.TabIndex = 3;
            // 
            // textQuantidade
            // 
            textQuantidade.Location = new Point(454, 260);
            textQuantidade.Name = "textQuantidade";
            textQuantidade.Size = new Size(100, 23);
            textQuantidade.TabIndex = 4;
            // 
            // textPreço
            // 
            textPreço.Location = new Point(454, 316);
            textPreço.Name = "textPreço";
            textPreço.Size = new Size(100, 23);
            textPreço.TabIndex = 5;
            // 
            // btnCadastrar
            // 
            btnCadastrar.Location = new Point(319, 397);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(194, 23);
            btnCadastrar.TabIndex = 6;
            btnCadastrar.Text = "Cadastrar Produto";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(538, 397);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(206, 23);
            btnBuscar.TabIndex = 7;
            btnBuscar.Text = "Buscar Produto";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1396, 824);
            Controls.Add(btnBuscar);
            Controls.Add(btnCadastrar);
            Controls.Add(textPreço);
            Controls.Add(textQuantidade);
            Controls.Add(textNome);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "EasyProdutos - Sistema de Vendas";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textNome;
        private TextBox textQuantidade;
        private TextBox textPreço;
        private Button btnCadastrar;
        private Button btnBuscar;
    }
}
