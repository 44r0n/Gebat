
// This file has been generated by the GUI designer. Do not modify.
namespace Gebat
{
	public partial class Cantidades
	{
		private global::Gtk.HBox hbox2;
		private global::Gtk.Fixed fixed3;
		private global::Gtk.Label label6;
		private global::Gtk.Entry entryType;
		private global::Gtk.Button buttonAccept;
		private global::Gtk.VBox vbox3;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Label label5;
		private global::Gtk.Entry entrySearch;
		private global::Gtk.VBox vbox4;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TreeView treeviewquantities;
		private global::Gtk.HBox hbox4;
		private global::Gtk.Button buttonDelete;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Gebat.Cantidades
			this.Name = "Gebat.Cantidades";
			this.Title = global::Mono.Unix.Catalog.GetString ("Cantidades");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child Gebat.Cantidades.Gtk.Container+ContainerChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.fixed3 = new global::Gtk.Fixed ();
			this.fixed3.Name = "fixed3";
			this.fixed3.HasWindow = false;
			// Container child fixed3.Gtk.Fixed+FixedChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Nombre: ");
			this.fixed3.Add (this.label6);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed3 [this.label6]));
			w1.X = 24;
			w1.Y = 183;
			// Container child fixed3.Gtk.Fixed+FixedChild
			this.entryType = new global::Gtk.Entry ();
			this.entryType.CanFocus = true;
			this.entryType.Name = "entryType";
			this.entryType.IsEditable = true;
			this.entryType.InvisibleChar = '●';
			this.fixed3.Add (this.entryType);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed3 [this.entryType]));
			w2.X = 87;
			w2.Y = 178;
			// Container child fixed3.Gtk.Fixed+FixedChild
			this.buttonAccept = new global::Gtk.Button ();
			this.buttonAccept.CanFocus = true;
			this.buttonAccept.Name = "buttonAccept";
			this.buttonAccept.UseUnderline = true;
			// Container child buttonAccept.Gtk.Container+ContainerChild
			global::Gtk.Alignment w3 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w4 = new global::Gtk.HBox ();
			w4.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w5 = new global::Gtk.Image ();
			w5.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w4.Add (w5);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w7 = new global::Gtk.Label ();
			w7.LabelProp = global::Mono.Unix.Catalog.GetString ("Aceptar");
			w7.UseUnderline = true;
			w4.Add (w7);
			w3.Add (w4);
			this.buttonAccept.Add (w3);
			this.fixed3.Add (this.buttonAccept);
			global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.fixed3 [this.buttonAccept]));
			w11.X = 116;
			w11.Y = 239;
			this.hbox2.Add (this.fixed3);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.fixed3]));
			w12.Position = 0;
			// Container child hbox2.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Buscar: ");
			this.hbox3.Add (this.label5);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label5]));
			w13.Position = 0;
			w13.Expand = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.entrySearch = new global::Gtk.Entry ();
			this.entrySearch.CanFocus = true;
			this.entrySearch.Name = "entrySearch";
			this.entrySearch.IsEditable = true;
			this.entrySearch.InvisibleChar = '●';
			this.hbox3.Add (this.entrySearch);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.entrySearch]));
			w14.Position = 1;
			this.vbox3.Add (this.hbox3);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox3]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeviewquantities = new global::Gtk.TreeView ();
			this.treeviewquantities.CanFocus = true;
			this.treeviewquantities.Name = "treeviewquantities";
			this.GtkScrolledWindow.Add (this.treeviewquantities);
			this.vbox4.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.GtkScrolledWindow]));
			w17.Position = 0;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonDelete = new global::Gtk.Button ();
			this.buttonDelete.Sensitive = false;
			this.buttonDelete.CanFocus = true;
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.UseUnderline = true;
			// Container child buttonDelete.Gtk.Container+ContainerChild
			global::Gtk.Alignment w18 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w19 = new global::Gtk.HBox ();
			w19.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w20 = new global::Gtk.Image ();
			w20.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-delete", global::Gtk.IconSize.Menu);
			w19.Add (w20);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w22 = new global::Gtk.Label ();
			w22.LabelProp = global::Mono.Unix.Catalog.GetString ("Eliminar");
			w22.UseUnderline = true;
			w19.Add (w22);
			w18.Add (w19);
			this.buttonDelete.Add (w18);
			this.hbox4.Add (this.buttonDelete);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.buttonDelete]));
			w26.Position = 1;
			w26.Expand = false;
			w26.Fill = false;
			this.vbox4.Add (this.hbox4);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.hbox4]));
			w27.Position = 1;
			w27.Expand = false;
			w27.Fill = false;
			this.vbox3.Add (this.vbox4);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.vbox4]));
			w28.Position = 1;
			this.hbox2.Add (this.vbox3);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.vbox3]));
			w29.Position = 1;
			this.Add (this.hbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 657;
			this.DefaultHeight = 448;
			this.Show ();
			this.buttonAccept.Clicked += new global::System.EventHandler (this.InsQuant);
			this.entrySearch.Changed += new global::System.EventHandler (this.searchType);
			this.treeviewquantities.CursorChanged += new global::System.EventHandler (this.treeTypeSelected);
			this.buttonDelete.Clicked += new global::System.EventHandler (this.deleteType);
		}
	}
}
