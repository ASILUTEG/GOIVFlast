using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GOIVF.Class
{
    public class clsConnectionNode
    {
        public string CnString;
        public DataTable sqlDT = new DataTable();
        private String BusinessName;
        private string BusAddress;
        private string BusContactNo;
        private string BusEmail;
        private string BusVatReg;

        // Initializing Database Connection
        public bool DBConnectionInitializing()
        {
            bool functionReturnValue = false;
            CnString = "Data Source=DESKTOP-1O4CHS8;Initial Catalog=GOIVF_DB;Integrated Security=True";
            try
            {
                SqlConnection sqlCon = new SqlConnection();
                sqlCon.ConnectionString = CnString;
                sqlCon.Open();
                functionReturnValue = true;
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                functionReturnValue = false;
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error establishing the database connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
            }
            return functionReturnValue;
        }


#if DEBUG
        // For benchmarking: allow injection of a custom IDbConnection
        public Func<string, IDbConnection> ConnectionFactory = connStr => new SqlConnection(connStr);

        /// <summary>
        /// Opens a file dialog for the user to select an image and loads it into the specified PictureBox.
        /// </summary>
        /// <param name="targetPictureBox">The PictureBox where the image will be loaded.</param>
        /// <param name="allowResize">Optional: Resize the image to fit the PictureBox.</param>
        public void LoadImageToPictureBox(PictureBox targetPictureBox, bool allowResize = true)
        {
            if (targetPictureBox == null)
                throw new ArgumentNullException(nameof(targetPictureBox), "Target PictureBox cannot be null.");

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Image";
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image selectedImage = Image.FromFile(openFileDialog.FileName);

                        if (allowResize)
                        {
                            targetPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            targetPictureBox.SizeMode = PictureBoxSizeMode.Normal;
                        }

                        targetPictureBox.BackgroundImage = selectedImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image:\n{ex.Message}", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

#endif
        /// <summary>
        /// Dynamically loads an image from a DataRow or DataGridView cell into a PictureBox.
        /// </summary>
        /// <param name="source">DataRow or DataGridView object</param>
        /// <param name="column">Column name (for DataRow) or column index (for DataGridView)</param>
        /// <param name="pictureBox">The PictureBox to display the image</param>
        /// <param name="sizeMode">Optional SizeMode for the PictureBox (default: Zoom)</param>
        public  void LoadImage(object source, object column, PictureBox pictureBox)
        {
            if (pictureBox == null)
                throw new ArgumentNullException(nameof(pictureBox));

            byte[] imageData = null;

            try
            {
                if (source is DataRow row)
                {
                    if (column is string columnName &&
                        row.Table.Columns.Contains(columnName) &&
                        row[columnName] != DBNull.Value)
                    {
                        imageData = row[columnName] as byte[];
                    }
                }
                else if (source is DataGridView dgv)
                {
                    if (dgv.CurrentRow != null &&
                        column is int columnIndex &&
                        dgv.CurrentRow.Cells[columnIndex].Value != DBNull.Value)
                    {
                        imageData = dgv.CurrentRow.Cells[columnIndex].Value as byte[];
                    }
                }

                // Load image if valid data
                if (imageData != null && imageData.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        pictureBox.BackgroundImage = Image.FromStream(ms);
                        pictureBox.SizeMode =PictureBoxSizeMode.StretchImage  ;
                    }
                }
                else
                {
                    pictureBox.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load image:\n{ex.Message}", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBox.Image = null;
            }
        }
        public DataTable GetDataTableFromQuery(string sql, Dictionary<string, object> parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(CnString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (parameters != null)
                {
                    foreach (var kvp in parameters)
                    {
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value ?? DBNull.Value);
                    }
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
        public void UpdateDataGridViewFromDataTable(DataGridView dgv, DataTable data)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                

                string productName = row.Cells["Column2"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(productName)) continue;

                // Find matching row in the offline data table
                DataRow[] matches = data.Select($"Product_Name = '{productName.Replace("'", "''")}'");
                if (matches.Length > 0)
                {
                    DataRow match = matches[0];

                    // Update grid row values
                    row.Cells["Column4"].Value = match["Category_Name"];
                    row.Cells["cmbSupplier"].Value = match["company_Id"];
                    row.Cells["Column3"].Value = match["Product_Bracode"];
                    row.Cells["colqty"].Value = match["Quantity"];
                    row.Cells["colunitepice"].Value = match["UnitPrice"];
                    row.Cells["colcheck"].Value = true;
                    string companyName = match["company_Name"].ToString();
                    DataTable companies = (DataTable)((DataGridViewComboBoxColumn)dgv.Columns["cmbSupplier"]).DataSource;

                    // Find the company ID by name
                    var matchRow = companies.AsEnumerable()
                                            .FirstOrDefault(r => r["company_Name"].ToString() == companyName);

                    if (matchRow != null)
                    {
                        object companyId = matchRow["company_Id"];  // 👈 what the ComboBox expects as Value
                        row.Cells["cmbSupplier"].Value = companyId; // ✅ This will work if the ID exists in the DataSource
                    }
                    else
                    {
                        Console.WriteLine($"Company '{companyName}' not found in ComboBox data source.");
                    }



                } else {
                    
                    
                    
                    row.Cells["colqty"].Value =0;
                    row.Cells["colunitepice"].Value =0;
                    row.Cells["colcheck"].Value = false;
                }
            }

            dgv.Refresh(); // Optional UI refresh
        }

        public bool DeleteRecord(string tableName, Dictionary<string, object> where)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(CnString))
                {
                    conn.Open();

                    // Build WHERE clause with ?
                    List<string> whereClauses = new List<string>();
                    foreach (var kvp in where)
                    {
                        whereClauses.Add($"{kvp.Key} = ?");
                    }

                    string sql = $"DELETE FROM {tableName} WHERE {string.Join(" AND ", whereClauses)}";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        // Add parameters in the SAME order as the WHERE clause
                        foreach (var kvp in where)
                        {
                            cmd.Parameters.AddWithValue("?", kvp.Value ?? DBNull.Value);
                        }

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Database Error",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        public int GetCount(string tableName, string idColumn, string condition = null)
        {
            string sql = $"SELECT COUNT({idColumn}) AS CNT FROM {tableName}";
            if (!string.IsNullOrEmpty(condition))
                sql += $" WHERE {condition}";

            ExecuteSQLQuery(sql);

            if (sqlDT.Rows.Count > 0 && sqlDT.Rows[0]["CNT"] != DBNull.Value)
                return Convert.ToInt32(sqlDT.Rows[0]["CNT"]);
            else
                return 0;
        }
        // GET MAX ID
        public int GetmAX(string tableName, string idColumn, string condition = null)
        {
            string sql = $"SELECT MAX({idColumn}) AS MX FROM {tableName}";
            if (!string.IsNullOrEmpty(condition))
                sql += $" WHERE {condition}";

            ExecuteSQLQuery(sql);

            if (sqlDT.Rows.Count > 0 && sqlDT.Rows[0]["MX"] != DBNull.Value)
                return Convert.ToInt32( sqlDT.Rows[0]["MX"])+1;
            else
                return 1;
        }


        public void UpdateProductPricesFromGrid(DataGridView dgv)
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("No data to update.", "GoPOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SqlConnection conn = new SqlConnection(CnString))
            {
                conn.Open();

                // Prepare reusable commands (better performance)
                string checkSql = "SELECT COUNT(*) FROM ProductPrice WHERE Product_ID=@Product_ID AND Company_ID=@Company_ID";
                string updateSql = @"UPDATE ProductPrice 
                             SET Price=@Price, PriceDate=GETDATE() 
                             WHERE Product_ID=@Product_ID AND Company_ID=@Company_ID";
                string insertSql = @"INSERT INTO ProductPrice (Product_ID, Company_ID, Price, PriceDate)
                             VALUES (@Product_ID, @Company_ID, @Price, GETDATE())";

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    try
                    {
                        int productId = Convert.ToInt32(row.Cells["Product_ID"].Value);
                        int companyId = Convert.ToInt32(row.Cells["Company_ID"].Value);
                        decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                        using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@Product_ID", productId);
                            checkCmd.Parameters.AddWithValue("@Company_ID", companyId);

                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (count > 0)
                            {
                                using (SqlCommand updateCmd = new SqlCommand(updateSql, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@Price", price);
                                    updateCmd.Parameters.AddWithValue("@Product_ID", productId);
                                    updateCmd.Parameters.AddWithValue("@Company_ID", companyId);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
                                {
                                    insertCmd.Parameters.AddWithValue("@Product_ID", productId);
                                    insertCmd.Parameters.AddWithValue("@Company_ID", companyId);
                                    insertCmd.Parameters.AddWithValue("@Price", price);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating Product ID {row.Cells["Product_ID"].Value}: {ex.Message}",
                                        "GoPOS Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                MessageBox.Show("✅ Product prices updated successfully!", "GoPOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool ExecuteStoredProcedure(string procName, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(CnString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters if provided
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                            }
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing stored procedure '" + procName + "': " + ex.Message,
                                "GoPOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        public void FilterDataGrid(DataGridView grid,DataTable dt, string searchText, params string[] columns)
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                    return;

                DataView dv = new DataView(dt);

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dv.RowFilter = "";
                    grid.DataSource = dv;
                    return;
                }

                string safeText = searchText.Replace("'", "''");

                List<string> filters = new List<string>();

                // Choose which columns to search
                var targetColumns = (columns != null && columns.Length > 0)
                    ? dt.Columns.Cast<DataColumn>().Where(c => columns.Contains(c.ColumnName))
                    : dt.Columns.Cast<DataColumn>();

                foreach (DataColumn col in targetColumns)
                {
                    // Always convert column to string safely
                    string condition = $"CONVERT([{col.ColumnName}], System.String) LIKE '%{safeText}%'";
                    filters.Add(condition);
                }

                dv.RowFilter = string.Join(" OR ", filters);
                grid.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering data: " + ex.Message,
                                "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public DataTable ExecuteSQLQuery(string SQLQuery)
        {
            try
            {
#if DEBUG
                var sqlCon = ConnectionFactory(CnString);
#else
                SqlConnection sqlCon = new SqlConnection(CnString);
#endif
                SqlDataAdapter sqlDA = new SqlDataAdapter(SQLQuery, (SqlConnection)sqlCon);
                SqlCommandBuilder sqlCB = new SqlCommandBuilder(sqlDA);
                sqlDT.Reset();
                sqlDA.Fill(sqlDT);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sqlDT;
        }
        public DataTable FillDataGridAndDataSet(string sql, DataGridView dgv)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(CnString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adp.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


        public void FillDataGrid(string sql, DataGridView dgv)
        {
            SqlConnection conn = new SqlConnection(CnString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adp = new SqlDataAdapter();
                DataTable dt = new DataTable();
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                dgv.DataSource = dt;
                adp.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        // DataGridView ComboBox 
        public void FillComboBoxColumn(string sql, string Value_Member, string Display_Member, DataGridViewComboBoxColumn combo)
        {
                SqlConnection connection =new SqlConnection(CnString);
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter(); 
                DataSet ds = new DataSet();
                try { 
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(ds);
                    adapter.Dispose(); 
                    command.Dispose(); 
                    connection.Close();
                    combo.DataSource = ds.Tables[0];
                    combo.DisplayMember = Display_Member;
                    combo.ValueMember = Value_Member;
                }
                catch (Exception ex) 
                {
                    System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
        }
        // End DataGridView ComboBox
        public void UpdateMainTableBasedOnSubTableConditions(string mainTable,
                                                            string mainTableKey,
                                                            string subTable,
                                                            string subTableKey,
                                                            Dictionary<string, (bool Apply, string Operator, object Value)> subConditions,
                                                            string fieldToUpdate,
                                                            object newValue)
        {
            if (string.IsNullOrWhiteSpace(mainTable) || string.IsNullOrWhiteSpace(subTable))
                throw new ArgumentException("Table names cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(mainTableKey) || string.IsNullOrWhiteSpace(subTableKey))
                throw new ArgumentException("Key fields cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(fieldToUpdate))
                throw new ArgumentException("Field to update cannot be null or empty.");
            ExecuteSQLQuery($"update [{mainTable}] set [{fieldToUpdate}] =0");
            var whereClauses = new List<string>();
            var parameters = new List<SqlParameter>();
            int paramIndex = 0;

            foreach (var kvp in subConditions)
            {
                if (!kvp.Value.Apply) continue;

                string column = kvp.Key;
                string op = kvp.Value.Operator?.ToUpper();
                object value = kvp.Value.Value;

                if (op == "BETWEEN" && value is Array betweenValues && betweenValues.Length == 2)
                {
                    string param1 = "@param" + paramIndex++;
                    string param2 = "@param" + paramIndex++;
                    whereClauses.Add($"[{column}] BETWEEN {param1} AND {param2}");
                    parameters.Add(new SqlParameter(param1, betweenValues.GetValue(0) ?? DBNull.Value));
                    parameters.Add(new SqlParameter(param2, betweenValues.GetValue(1) ?? DBNull.Value));
                }
                else if (op == "IN" && value is IEnumerable<object> inValues)
                {
                    var paramNames = new List<string>();
                    foreach (var val in inValues)
                    {
                        string pname = "@param" + paramIndex++;
                        paramNames.Add(pname);
                        parameters.Add(new SqlParameter(pname, val ?? DBNull.Value));
                    }
                    whereClauses.Add($"[{column}] IN ({string.Join(", ", paramNames)})");
                }
                else
                {
                    string param = "@param" + paramIndex++;
                    whereClauses.Add($"[{column}] {op} {param}");
                    parameters.Add(new SqlParameter(param, value ?? DBNull.Value));
                }
            }

            string whereClause = whereClauses.Count > 0 ? "WHERE " + string.Join(" AND ", whereClauses) : "";

                            string sql = $@"
                        UPDATE {mainTable}
                        SET {fieldToUpdate} = @newValue
                        WHERE {mainTableKey} IN (
                            SELECT {subTableKey}
                            FROM {subTable}
                            {whereClause}
                        );
                    ";

            using (SqlConnection con = new SqlConnection(CnString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@newValue", newValue ?? DBNull.Value);
                cmd.Parameters.AddRange(parameters.ToArray());
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void FillControlsFromTable(string tableName, Dictionary<string, object> filters, Control.ControlCollection controls)
        {
            // Build SQL
            var sql = new StringBuilder($"SELECT * FROM [{tableName}]");
            var parameters = new List<SqlParameter>();

            if (filters?.Count > 0)
            {
                sql.Append(" WHERE ");
                var conditions = new List<string>();
                int i = 0;
                foreach (var kvp in filters)
                {
                    string paramName = "@param" + i++;
                    conditions.Add($"[{kvp.Key}] = {paramName}");
                    parameters.Add(new SqlParameter(paramName, kvp.Value ?? DBNull.Value));
                }
                sql.Append(string.Join(" AND ", conditions));
            }

            using (SqlConnection con = new SqlConnection(CnString))
            using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
            {
                cmd.Parameters.AddRange(parameters.ToArray());
                con.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No matching record found.");
                        return;
                    }

                    FillControlsFromDataRow(dt.Rows[0], controls);
                }
            }
        }
       

        private void FillControlsFromDataRow(DataRow row, Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                string name = control.Name;
                string colName = name;

                if (name.StartsWith("txt", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith("cmb", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith("chk", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith("dtp", StringComparison.OrdinalIgnoreCase))
                {
                    colName = name.Substring(3); // remove 3-letter prefix
                }

                if (row.Table.Columns.Contains(colName))
                {
                    object value = row[colName];

                    if (control is TextBox txt)
                    {
                        txt.Text = value?.ToString();
                    }
                    else if (control is ComboBox cmb)
                    {
                        cmb.Text = value?.ToString();
                    }
                    else if (control is CheckBox chk)
                    {
                        if (value != DBNull.Value)
                            chk.Checked = Convert.ToBoolean(value);
                    }
                    else if (control is DateTimePicker dtp)
                    {
                        if (value != DBNull.Value)
                            dtp.Value = Convert.ToDateTime(value);
                    }
                }

                // Handle nested controls (e.g., inside GroupBox or Panel)
                if (control.HasChildren)
                {
                    FillControlsFromDataRow(row, control.Controls);
                }
            }
        }







        // FillCombo Box Dinmically
        public void FillComboBox(string sql, string Value_Member, string Display_Member, ComboBox combo)
        {
            SqlConnection connection = new SqlConnection(CnString);
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                connection.Close();
                combo.DataSource = ds.Tables[0];
                combo.DisplayMember = Display_Member;
                combo.ValueMember = Value_Member;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        public int GetProductQuantity(int productId, string companyName)
        {
            ExecuteStoredProcedure("UpdateProductQuantities");
            ExecuteStoredProcedure("UpdateProductQuantitiesCompany");
            int quantity = 0;

            string query = @"SELECT ISNULL(Quantity, 0) FROM ProductPrice WHERE PRODUCT_ID = @ProductId  AND Company_Name = @CompanyName";

            try
            {
                using (SqlConnection conn = new SqlConnection(CnString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@CompanyName", companyName);

                    conn.Open();

                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        quantity = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting product quantity:\n" + ex.Message,
                                "GoPOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return quantity;
        }


        public double num_repl(string a) 
        {
            double n;
            bool isNumeric = double.TryParse(a, out n);
            return n;
        }


        public object str_repl(string str)
        {
            return str.Replace("'" , "");
        }

        public object fltr_combo(ComboBox cmbo)
        {
            if (cmbo.SelectedIndex == -1)
            {
                return 0;
            }
            return cmbo.SelectedValue;
        }

        //Genarate Auto Barcode
        public string GenarateAutoBarcode(string barcode)
        {
            int val = 0;
            ExecuteSQLQuery("SELECT * FROM Barcode");
            if (sqlDT.Rows.Count > 0) {
                barcode = sqlDT.Rows[0]["Barcode"].ToString();
                val = Int32.Parse(barcode) + 1 ;
                ExecuteSQLQuery("UPDATE  Barcode  SET  Barcode='" + val + "' ");
                barcode = val.ToString();
            }
            else {
                ExecuteSQLQuery("INSERT INTO Barcode (Barcode) VALUES ('100000') ");
                barcode = "100000";
            }
            return barcode;
        }

        // Filter DataTable Dynamically
        public DataTable FilterTable(
            string tableName,string fldsShow,
            Dictionary<string, (bool Apply, string Operator, object Value)> filters)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name cannot be null or empty.");

            if (!Regex.IsMatch(tableName, @"^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("Invalid table name.");

            var sql = new StringBuilder($"SELECT {fldsShow} FROM [{tableName}]");
            var parameters = new List<SqlParameter>();

            if (filters != null && filters.Count > 0)
            {
                sql.Append(" WHERE ");
                var filterClauses = new List<string>();
                int index = 0;

                foreach (var kvp in filters)
                {
                    string column = kvp.Key;
                    var (apply, op, value) = kvp.Value;

                    if (!apply)
                        continue; // Skip this filter

                    if (!Regex.IsMatch(column, @"^[a-zA-Z0-9_]+$"))
                        throw new ArgumentException($"Invalid column name: {column}");

                    op = op.Trim().ToUpperInvariant();

                    switch (op)
                    {
                        case "IN":
                            if (value is IEnumerable enumerable && !(value is string))
                            {
                                var inParams = new List<string>();
                                int inIndex = 0;

                                foreach (var item in enumerable)
                                {
                                    string inParamName = $"@param{index}_{inIndex++}";
                                    inParams.Add(inParamName);
                                    parameters.Add(new SqlParameter(inParamName, item ?? DBNull.Value));
                                }

                                if (inParams.Count == 0)
                                    filterClauses.Add("1 = 0"); // No values, match nothing
                                else
                                    filterClauses.Add($"[{column}] IN ({string.Join(", ", inParams)})");

                                index++;
                            }
                            else
                            {
                                throw new ArgumentException("Value for IN must be a non-string IEnumerable.");
                            }
                            break;

                        case "BETWEEN":
                            if (value is IEnumerable betweenVals && !(value is string))
                            {
                                var betweenList = betweenVals.Cast<object>().ToList();
                                if (betweenList.Count != 2)
                                    throw new ArgumentException("Value for BETWEEN must have exactly two elements.");

                                string paramName1 = $"@param{index}_start";
                                string paramName2 = $"@param{index}_end";

                                filterClauses.Add($"[{column}] BETWEEN {paramName1} AND {paramName2}");
                                parameters.Add(new SqlParameter(paramName1, betweenList[0] ?? DBNull.Value));
                                parameters.Add(new SqlParameter(paramName2, betweenList[1] ?? DBNull.Value));
                                index++;
                            }
                            else
                            {
                                throw new ArgumentException("Value for BETWEEN must be a non-string IEnumerable with two items.");
                            }
                            break;

                        default:
                            if (!IsValidSqlOperator(op))
                                throw new ArgumentException($"Invalid SQL operator: {op}");

                            string paramName = "@param" + index++;
                            filterClauses.Add($"[{column}] {op} {paramName}");
                            parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                            break;
                    }
                }

                sql.Append(string.Join(" AND ", filterClauses));
            }

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(CnString))
            using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
            {
                cmd.Parameters.AddRange(parameters.ToArray());
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        private bool IsValidSqlOperator(string op)
        {
                    var validOperators = new HashSet<string>
            {
                "=", "<>", "!=", "<", "<=", ">", ">=", "LIKE", "NOT LIKE", "IS", "IS NOT"
            };

            return validOperators.Contains(op.ToUpperInvariant());
        }

        // End Filter DataTable Dynamically
        // Copy ComboBox Items Dynamically
        public  void CopyComboBoxItems(ComboBox source, ComboBox target)
        {
            if (source == null || target == null)
                throw new ArgumentNullException("Source or target ComboBox is null.");

            // Copy data binding
            target.DataSource = source.DataSource;
            target.DisplayMember = source.DisplayMember;
            target.ValueMember = source.ValueMember;

            // Optional: set selected index/value
            target.SelectedIndex = source.SelectedIndex;
        }

        // End Copy ComboBox Items Dynamically



        // Upload Photo to Database Dynamically
        public void UploadPhotoToDatabase(string tableName,string photoColumnName, Image image,Dictionary<string, object> conditions)
        {
            if (image == null || conditions == null || conditions.Count == 0)
                throw new ArgumentException("Image and conditions are required.");

            using (SqlConnection con = new SqlConnection(CnString))
            {
                // Build WHERE clause and parameters
                var whereClauses = new List<string>();
                var parameters = new List<SqlParameter>();

                foreach (var condition in conditions)
                {
                    string paramName = $"@{condition.Key}";
                    whereClauses.Add($"[{condition.Key}] = {paramName}");
                    parameters.Add(new SqlParameter(paramName, condition.Value ?? DBNull.Value));
                }

                string whereClause = string.Join(" AND ", whereClauses);
                string sql = $"UPDATE [{tableName}] SET [{photoColumnName}] = @Photo WHERE {whereClause}";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    // Convert image to byte array
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, image.RawFormat);
                        byte[] data = ms.ToArray();

                        cmd.Parameters.Add("@Photo", SqlDbType.Image).Value = data;

                        // Add WHERE clause parameters
                        cmd.Parameters.AddRange(parameters.ToArray());

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // End Upload Photo to Database Dynamically


        public void DeleteRecordFromDatabase(string tableName, Dictionary<string, object> conditions)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name is required.", nameof(tableName));

            if (conditions == null || conditions.Count == 0)
                throw new ArgumentException("At least one condition is required to avoid deleting all records.");

            using (SqlConnection con = new SqlConnection(CnString))
            {
                // Build WHERE clause and parameters
                var whereClauses = new List<string>();
                var parameters = new List<SqlParameter>();

                foreach (var condition in conditions)
                {
                    string paramName = $"@{condition.Key}";
                    whereClauses.Add($"[{condition.Key}] = {paramName}");
                    parameters.Add(new SqlParameter(paramName, condition.Value ?? DBNull.Value));
                }

                string whereClause = string.Join(" AND ", whereClauses);
                string sql = $"DELETE FROM [{tableName}] WHERE {whereClause}";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }



        // Set TextBox to "0" if empty
        public void SetTextBoxZeroIfEmpty(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
            }
        }
        // End Set TextBox to "0" if empty
        // Check if Row Exists Dynamically
        public bool RowExists(string tableName, Dictionary<string, object> conditions)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name cannot be null or empty.");

            if (conditions == null || conditions.Count == 0)
                throw new ArgumentException("Conditions dictionary cannot be null or empty.");

            string whereClause = string.Join(" AND ", conditions.Keys.Select(k => $"[{k}] = @{k}"));
            string sql = $"SELECT 1 FROM [{tableName}] WHERE {whereClause}";

            using (SqlConnection con = new SqlConnection(CnString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                foreach (var kvp in conditions)
                {
                    cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? DBNull.Value);
                }

                con.Open();
                var result = cmd.ExecuteScalar();

                return result != null;
            }
        }
        // End Check if Row Exists Dynamically

        // Insert Record Dynamically
        public void InsertRecord(string tableName, Dictionary<string, object> columns)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name cannot be null or empty.");

            if (columns == null || columns.Count == 0)
                throw new ArgumentException("Columns dictionary cannot be null or empty.");

            // Build column list and parameter placeholders
            string columnList = string.Join(",", columns.Keys);
            string parameterList = string.Join(",", columns.Keys.Select(k => "@" + k));

            string sql = $"INSERT INTO [{tableName}] ({columnList}) VALUES ({parameterList})";

            using (SqlConnection con = new SqlConnection(CnString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                foreach (var kvp in columns)
                {
                    
                    cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? 0);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // End Insert Record Dynamically

        // Update Record Dynamically
        public void UpdateRecord(string tableName, Dictionary<string, object> columnsToUpdate, Dictionary<string, object> whereConditions)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name cannot be null or empty.");

            if (columnsToUpdate == null || columnsToUpdate.Count == 0)
                throw new ArgumentException("Columns to update cannot be null or empty.");

            if (whereConditions == null || whereConditions.Count == 0)
                throw new ArgumentException("Where conditions cannot be null or empty.");

            // Build SET clause
            string setClause = string.Join(", ", columnsToUpdate.Keys.Select(k => $"[{k}] = @{k}"));

            // Build WHERE clause
            string whereClause = string.Join(" AND ", whereConditions.Keys.Select(k => $"[{k}] = @w_{k}"));

            string sql = $"UPDATE [{tableName}] SET {setClause} WHERE {whereClause}";

            using (SqlConnection con = new SqlConnection(CnString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                // Add parameters for SET
                foreach (var kvp in columnsToUpdate)
                {
                    cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? DBNull.Value);
                }

                // Add parameters for WHERE (prefix with @w_ to avoid name conflict)
                foreach (var kvp in whereConditions)
                {
                    cmd.Parameters.AddWithValue("@w_" + kvp.Key, kvp.Value ?? DBNull.Value);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // End Update Record Dynamically

        // Generate Access Number
        public string GenerateAccessNumber(string prefix, DateTime? date = null, int lastNumber = 0)
        {
            DateTime currentDate = date ?? DateTime.Now;
            string datePart = currentDate.ToString("yyyyMMdd");  // e.g. 20251003
            int nextNumber = lastNumber ;
            string formattedNumber = nextNumber.ToString("D4");  // 0001, 0002, etc.
            string accessNumber = $"{prefix}-{datePart}-{formattedNumber}";
            return accessNumber;
        }
        // End Generate Access Number


        // Get Code by Name Dynamically
        public int GetCodeByName(string tableName, string fieldName, string fieldValue, string codeField)
        {
            if (string.IsNullOrWhiteSpace(tableName) ||
                string.IsNullOrWhiteSpace(fieldName) ||
                string.IsNullOrWhiteSpace(codeField))
            {
                throw new ArgumentException("Table name, field name, and code field cannot be null or empty.");
            }

            string sql = $"SELECT [{codeField}] FROM [{tableName}] WHERE [{fieldName}] = @FieldValue";

            using (SqlConnection con = new SqlConnection(CnString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@FieldValue", fieldValue);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int code))
                {
                    return code;
                }
                else
                {
                    // Return -1 or throw if not found
                    return -1;
                }
            }
        }
        // End Get Code by Name Dynamically


        //Report view..................
       
        public void CompanyInfo() {
            ExecuteSQLQuery("SELECT   *    FROM   BusinessInfo");
            if (sqlDT.Rows.Count > 0)
            {
                BusinessName = sqlDT.Rows[0]["BusinessName"].ToString();
                BusAddress = sqlDT.Rows[0]["Address"].ToString();
                BusContactNo = sqlDT.Rows[0]["ContactNo"].ToString();
                BusEmail = sqlDT.Rows[0]["Email"].ToString();
                BusVatReg = sqlDT.Rows[0]["VatRegNo"].ToString();
            }
            else {
                BusinessName = "Company Name";
                BusAddress = "Company Address";
                BusContactNo = "Contact No";
                BusEmail = "Your Email";
                BusVatReg = "00-000-00-00";
            }
        }
        public bool DeleteInvoice(int invoiceId)
        {
            if (invoiceId <= 0)
            {
                MessageBox.Show("Invalid Invoice ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 🟡 Confirm delete
            DialogResult confirm = MessageBox.Show(
                "Do you really want to delete this invoice and all related data?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return false;

            try
            {
                // ✅ Check if invoice exists first
                if (!RowExists("Invoices", new Dictionary<string, object> { { "InvoiceId", invoiceId } }))
                {
                    MessageBox.Show("Invoice not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                using (SqlConnection con = new SqlConnection(CnString))
                {
                    con.Open();
                    using (SqlTransaction trans = con.BeginTransaction())
                    {
                        try
                        {
                            // 1️⃣ Delete related products
                            using (SqlCommand cmd1 = new SqlCommand("DELETE FROM InvoiceProduct WHERE InvoiceId = @InvoiceId", con, trans))
                            {
                                cmd1.Parameters.AddWithValue("@InvoiceId", invoiceId);
                                cmd1.ExecuteNonQuery();
                            }

                            // 2️⃣ Delete related images
                            using (SqlCommand cmd2 = new SqlCommand("DELETE FROM inoviceimages WHERE inoviceId = @InvoiceId", con, trans))
                            {
                                cmd2.Parameters.AddWithValue("@InvoiceId", invoiceId);
                                cmd2.ExecuteNonQuery();
                            }

                            // 3️⃣ Delete main invoice
                            using (SqlCommand cmd3 = new SqlCommand("DELETE FROM Invoices WHERE InvoiceId = @InvoiceId", con, trans))
                            {
                                cmd3.Parameters.AddWithValue("@InvoiceId", invoiceId);
                                int rows = cmd3.ExecuteNonQuery();

                                trans.Commit();

                                if (rows > 0)
                                {
                                    MessageBox.Show("Invoice and all related data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show("Invoice not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            MessageBox.Show("Error deleting invoice: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable getdatatb(string sql)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(CnString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product list data:\n" + ex.Message);
            }

            return dt;
        }

                // Fill random data in all tables
        public void FillRandomDataInAllTables(int rowsPerTable = 10)
        {
            using (SqlConnection conn = new SqlConnection(CnString))
            {
                conn.Open();
                // Get all table names
                var tableCmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", conn);
                var tables = new List<string>();
                using (var reader = tableCmd.ExecuteReader())
                {
                    while (reader.Read())
                        tables.Add(reader.GetString(0));
                }
                Random rnd = new Random();
                foreach (var table in tables)
                {
                    // Get columns and types
                    var colCmd = new SqlCommand($"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}'", conn);
                    var columns = new List<(string, string)>();
                    using (var reader = colCmd.ExecuteReader())
                    {
                        while (reader.Read())
                            columns.Add((reader.GetString(0), reader.GetString(1)));
                    }
                    for (int i = 0; i < rowsPerTable; i++)
                    {
                        var colNames = string.Join(", ", columns.Select(c => c.Item1));
                        var values = string.Join(", ", columns.Select(c => GenerateRandomValue(c.Item2, rnd)));
                        var insertCmd = new SqlCommand($"INSERT INTO [{table}] ({colNames}) VALUES ({values})", conn);
                        try { insertCmd.ExecuteNonQuery(); } catch { /* skip errors for identity/required columns */ }
                    }
                }
            }
        }
        public int SafeToInt(string input, int defaultValue = 0)
        {
            if (int.TryParse(input, out int result))
                return result;
            else
                return defaultValue;
        }

        private string GenerateRandomValue(string dataType, Random rnd)
        {
            switch (dataType.ToLower())
            {
                case "int": return rnd.Next(1, 10000).ToString();
                case "bigint": return rnd.Next(1, 100000).ToString();
                case "float": return rnd.NextDouble().ToString();
                case "decimal": return ((decimal)rnd.NextDouble() * 1000).ToString();
                case "bit": return (rnd.Next(0, 2) == 1 ? "1" : "0");
                case "date": return $"'2023-{rnd.Next(1,12):D2}-{rnd.Next(1,28):D2}'";
                case "datetime": return $"'2023-{rnd.Next(1,12):D2}-{rnd.Next(1,28):D2} {rnd.Next(0,23):D2}:{rnd.Next(0,59):D2}:00'";
                case "nvarchar":
                case "varchar": return $"'Random{rnd.Next(1000,9999)}'";
                case "char": return $"'C{rnd.Next(10,99)}'";
                default: return "NULL";
            }
        }

        //////////////////////////////////////////////////
    }

}
